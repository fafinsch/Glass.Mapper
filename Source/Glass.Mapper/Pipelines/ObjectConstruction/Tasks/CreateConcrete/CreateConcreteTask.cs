﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;
using Glass.Mapper.Profilers;

namespace Glass.Mapper.Pipelines.ObjectConstruction.Tasks.CreateConcrete
{
    public class CreateConcreteTask : IObjectConstructionTask
    {
        private const string ConstructorErrorMessage = "No constructor for class {0} with parameters {1}";

        private static volatile  ProxyGenerator _generator;
        private static volatile  ProxyGenerationOptions _options;

        static CreateConcreteTask()
        {
            _generator = new ProxyGenerator();
            var hook = new LazyObjectProxyHook();
            _options = new ProxyGenerationOptions(hook);
        }

        public void Execute(ObjectConstructionArgs args)
        {
            if (args.Result != null)
                return;

            var type = args.Configuration.Type;

            if(type.IsInterface)
            {
                return;
            }

            if(args.AbstractTypeCreationContext.IsLazy)
            {
                //here we create a lazy loaded version of the class
                args.Result = CreateLazyObject(args);
                args.AbortPipeline();

            }
            else
            {
                //here we create a concrete version of the class
                args.Result = CreateObject(args);
                args.AbortPipeline();

                
            }
        }

        protected virtual object CreateLazyObject(ObjectConstructionArgs args)
        {
            return  _generator.CreateClassProxy(args.Configuration.Type, new LazyObjectInterceptor(args));
        }

        protected virtual object CreateObject(ObjectConstructionArgs args)
        {
            var constructorParameters = args.AbstractTypeCreationContext.ConstructorParameters;

            var parameters = 
                constructorParameters == null || !constructorParameters.Any() ? Type.EmptyTypes : constructorParameters.Select(x => x.GetType()).ToArray();

            var constructorInfo = args.AbstractTypeCreationContext.RequestedType.GetConstructor(parameters);

            Delegate conMethod = args.Configuration.ConstructorMethods[constructorInfo];

            var obj = conMethod.DynamicInvoke(constructorParameters);
         
            //create properties 
            AbstractDataMappingContext dataMappingContext =  args.Service.CreateDataMappingContext(args.AbstractTypeCreationContext, obj);

            foreach (var prop in args.Configuration.Properties)
            {
                prop.Mapper.MapCmsToProperty(dataMappingContext);
            }

            return obj;
        }
    }
}