/*
Copyright (c) .NET Foundation and Contributors

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

I (Marc Lohrer) changed the file.

This notice is intended to comply with the Apache Licence 2. 0 section 4.b. that states

"4. You may reproduce and distribute copies of the Work or Derivative Works thereof in any medium, 
 with or without modifications, and in Source or Object form, provided that You meet the following conditions:
 ... 
 b. You must cause any modified files to carry prominent notices stating that You changed the files; and
 "
*/
namespace StiebelEltronDashboard
{
    using Serilog.Core;
    using Serilog.Events;
    using System;
    using System.Collections.Generic;

    internal class LoggingFilter : ILogEventFilter
    {
        private static readonly HashSet<string> ignoredMessages = new HashSet<string>(StringComparer.Ordinal)
        {
            "Start processing HTTP request {HttpMethod} {Uri}",
            "End processing HTTP request after {ElapsedMilliseconds}ms - {StatusCode}"
        };

        // Allow the event to be logged if the message template isn't one we ignore
        public bool IsEnabled(LogEvent logEvent) => !ignoredMessages.Contains(logEvent.MessageTemplate.Text);
    }
}