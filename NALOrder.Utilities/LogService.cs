// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 04-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 04-6-2016
// ***********************************************************************

using log4net;
using System;
using System.Collections.Generic;

namespace NALOrder.Utilities
{
    public class LogService : ILogService
    {
        private ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Error(object message, Exception e)
        {
            _logger.Error(message, e);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(object message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Info(object message, Exception e)
        {
            _logger.Info(message, e);
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(object message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="e">The e.</param>
        public void Warning(object message, Exception e)
        {
            _logger.Warn(message, e);
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warning(object message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// Set some addtional attributes that will be logged by log4net
        /// </summary>
        /// <param name="attributes"></param>
        public void SetAdditionalAttributes(Dictionary<string, object> attributes)
        {
            if (attributes == null)
                return;
            foreach (var key in attributes.Keys)
            {
                log4net.ThreadContext.Properties[key] = attributes;
            }
        }
    }
}
