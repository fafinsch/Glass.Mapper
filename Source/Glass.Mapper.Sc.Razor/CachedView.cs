/*
   Copyright 2012 Michael Edwards
 
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 
*/ 
//-CRE-
using System;
using RazorEngine.Templating;

namespace Glass.Mapper.Sc.Razor
{

    /// <summary>
    /// CachedView
    /// </summary>
    public class CachedView
    {
        /// <summary>
        /// Gets or sets the content of the view.
        /// </summary>
        /// <value>
        /// The content of the view.
        /// </value>
        public string ViewContent { get; set; }

        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>
        /// The template.
        /// </value>
        public ITemplate Template { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}

