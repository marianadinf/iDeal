using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIT.iDeal.Common.Extensions;

namespace UIT.iDeal.Common.Errors
{
    // <summary>

    /// <summary>
    /// Container for maintaining and displaying user friendly distinct application messages
    /// </summary>
    public class ExecutionResult : IEnumerable<KeyValuePair<MessageCategory, MessageGroup>>
    {
        private readonly object _executedObject;

        #region Members

        /// <summary>
        /// Internal structure to hold a list of messages
        /// </summary>        
        private readonly Dictionary<MessageCategory, MessageGroup> _messages = new Dictionary<MessageCategory, MessageGroup>();

        #endregion

        #region Properties

        /// <summary>
        /// Returns all the message available as KeyValuePair of message type and messages (sorted by less to more critical message)
        /// </summary>
        public IEnumerable<KeyValuePair<MessageCategory, MessageGroup>> All
        {
            get
            {
                return _messages
                        .ToList()
                        .OrderBy(i => i.Key);
            }
        }

        /// <summary>
        /// Checks if no message type with high severity has been specified
        /// </summary>
        public Boolean IsSuccessFull
        {
            get
            {
                //Any message type greater than information or warning 
                return !_messages.Any(m => m.Key > MessageCategory.Warning);
            }
        }

        /// <summary>
        /// Checks if no message has been specified
        /// </summary>
        public Boolean IsEmpty
        {
            get { return !_messages.Any(); }
        }


        /// <summary>
        /// Checks if there is at least a message of type fatal error 
        /// </summary>
        public Boolean HasFatalError
        {
            get
            {
                return _messages.ContainsKey(MessageCategory.FatalException) &&
                       _messages[MessageCategory.FatalException].Messages.Any();
            }
        }

        /// <summary>
        /// Checks if there is at leat one message of type authorisation error
        /// </summary>
        public Boolean HasAuthorisationError
        {
            get
            {
                return _messages.ContainsKey(MessageCategory.AuthorisationException) &&
                       _messages[MessageCategory.AuthorisationException].Messages.Any();

            }
        }

        /// <summary>
        /// Checks if there errors (either authorisation or fatal errors)
        /// </summary>
        public Boolean HasError
        {
            get
            {
                return HasFatalError || HasAuthorisationError;
            }
        }

        /// <summary>
        /// Returns all errors (authorisation, business errors and fatal exception combined) as a collection of KeyValuePair of Message category / MessageGroup
        /// </summary>
        public IEnumerable<KeyValuePair<MessageCategory, MessageGroup>> Errors
        {
            get
            {
                return _messages
                        .Where(m => m.Key > MessageCategory.Warning)
                        .OrderBy(m => m.Key)
                        .ToList();
            }
        }
        /// <summary>
        /// Returns all errors (authorisation and fatal exception combined) and broken business rules
        /// into a combined message group for displaying purposes
        /// </summary>
        public MessageGroup ErrorsMessageGroup
        {
            get
            {
                var items = _messages
                                .Where(m => m.Key > MessageCategory.Warning)
                                .OrderBy(m => m.Key)
                                .ToList();

                var list = new List<String>();

                foreach (var item in items)
                {
                    list.AddRange(item.Value.Messages);
                }
                var result = new MessageGroup(list, MessageCategory.BrokenBusinessRule.ToTitle());

                return result;
            }
        }

        /// <summary>
        /// Returns all errors (authorisation and fatal exception combined) as a collection of string messages
        /// </summary>
        public IEnumerable<String> AllErrorMessages
        {
            get
            {
                return Errors.SelectMany(x => x.Value.Messages);
            }
        }

        /// <summary>
        /// Get a message group for a specific message type
        /// </summary>
        /// <param name="messageCategory">a message category</param>
        /// <returns>a collection of messages with a title</returns>
        public MessageGroup this[MessageCategory messageCategory]
        {
            get
            {
                return GetMessageGroup(messageCategory);
            }
        }

        /// <summary>
        /// What is affected by this message
        /// </summary>
        public IEnumerable<String> WhatIsAffected { get; set; }

        /// <summary>
        /// What the user can do from there
        /// </summary>
        public IEnumerable<String> WhatTheUserCanDo { get; set; }


        #endregion

        #region Public Methods

        #region Constructors

        /// <summary>
        /// Builds a empty application message instance
        /// </summary>
        public ExecutionResult()
        { }

        /// <summary>
        /// Builds a empty application message instance
        /// </summary>
        public ExecutionResult(object executedObject)
        {
            _executedObject = executedObject;
        }


        /// <summary>
        /// Build a new application message with single message against a message type
        /// </summary>
        /// <param name="messageCategory">type of message which can fall into 4 categories (informative, warning, broken bussiness rule and fatal system exception</param>
        /// <param name="message">
        ///     User friendly message that gives a clear description of what the message type means
        ///     More than one message can be added to the same message category
        /// </param>
        public ExecutionResult(MessageCategory messageCategory, String message)
        {
            Add(messageCategory, message, null);
        }


        /// <summary>
        /// Build a new application message with a list of message description for a message category
        /// </summary>
        /// <param name="messageCategory">type of message which can fall into 4 categories (informative, warning, broken bussiness rule and fatal system exception</param>
        /// <param name="message">List of messages to be added to this message type</param>
        public ExecutionResult(MessageCategory messageCategory, MessageGroup messageGroup)
        {
            Add(messageCategory, messageGroup);
        }

        /// <summary>
        /// Merge another application message into this newly built one
        /// </summary>
        /// <param name="executionResult"></param>
        public ExecutionResult(ExecutionResult executionResult)
        {
            Merge(executionResult);
        }

        #endregion


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Type: {0}", _executedObject.GetType().Name));
            sb.AppendLine(string.Format("Execution Status: {0}", IsSuccessFull ? "Success" : "Failure"));
            if (!IsSuccessFull)
            {
                sb.Append(AllErrorMessages);
            }
            return sb.ToString();
        }
     
        /// <summary>
        /// Merge another application message into the current one
        /// </summary>
        /// <param name="executionResult"></param>
        public ExecutionResult Merge(ExecutionResult executionResult)
        {
            if (executionResult != null)
            {
                foreach (var item in executionResult.All)
                {
                    Add(item.Key, item.Value);
                }
            }

            #region append what is affected and what the user can do

            var whatIsAffected = new List<String>();

            if (executionResult.WhatIsAffected != null)
            {
                whatIsAffected = executionResult.WhatIsAffected.ToList();
            }

            if (this.WhatIsAffected != null)
            {
                var list = this.WhatIsAffected.ToList();

                list.AddRange(whatIsAffected);

                whatIsAffected = list;
            }

            this.WhatIsAffected = whatIsAffected;

            var whatTheUserCanDo = new List<String>();

            if (executionResult.WhatTheUserCanDo != null)
            {
                whatTheUserCanDo = executionResult.WhatTheUserCanDo.ToList();
            }

            if (this.WhatTheUserCanDo != null)
            {
                var list = this.WhatTheUserCanDo.ToList();
                list.AddRange(whatTheUserCanDo);

                whatTheUserCanDo = list;
            }

            this.WhatTheUserCanDo = whatTheUserCanDo;

            return this;
        }


        /// <summary>
        /// Add a list of message description for a message category
        /// </summary>
        /// <param name="messageCategory">type of message which can fall into 4 categories (informative, warning, broken bussiness rule and fatal system exception</param>
        /// <param name="messageGroup">List of messages to be added to this message type</param>
        public void Add(MessageCategory messageCategory, MessageGroup messageGroup)
        {
            if ((messageGroup == null) || (!messageGroup.Messages.Any()))
            {
                messageCategory = MessageCategory.FatalException;

                String message;
                if (messageGroup == null)
                {
                    message = "No message description has been specified";
                    messageGroup = new MessageGroup();
                }
                else
                {
                    message = messageGroup.Messages.Any(String.IsNullOrWhiteSpace)
                                  ? "All message description must be specified"
                                  : "No message type has been specified";
                }
                messageGroup.Messages.Add(message);
            }

            if (_messages.ContainsKey(messageCategory))
            {
                foreach (var m in messageGroup.Messages)
                {
                    if (!_messages[messageCategory].Messages.Contains(m))
                    {
                        _messages[messageCategory].Messages.Add(m);
                    }
                }

            }
            else
            {
                _messages.Add(messageCategory, messageGroup.Clone());
            }
        }

        /// <summary>
        /// Add a new message description into a message category
        /// </summary>
        /// <param name="messageCategory">type of message which can fall into 4 categories (informative, warning, broken bussiness rule and fatal system exception</param>
        /// <param name="message">
        ///     User friendly message that gives a clear description of what the message type means
        ///     More than one message can be added to the same message category
        /// </param>
        public void Add(MessageCategory messageCategory, String message, String propertyName = null)
        {
            Add(messageCategory, new MessageGroup(new List<String> {message}, propertyName));
        }

        /// <summary>
        /// Checks if a message type exists
        /// </summary>
        /// <param name="messageCategory">type of message which can fall into 4 categories (informative, warning, broken bussiness rule and fatal system exception</param>
        /// <returns>Boolean</returns>
        public Boolean Contains(MessageCategory messageCategory)
        {
            return _messages.ContainsKey(messageCategory);
        }

        /// <summary>
        /// Checks if a message exists (regardless of the category it belongs to) 
        /// </summary>
        /// <param name="message">message itself</param>
        /// <returns>Boolean</returns>
        public Boolean Contains(String message)
        {
            return _messages.Values.Any(m => m.Messages.Contains(message));
        }

        /// <summary>
        /// Get all the messages for specific message type
        /// </summary>
        /// <param name="messageCategory"></param>
        /// <returns></returns>
        public IEnumerable<String> GetMessages(MessageCategory messageCategory)
        {

            var messageGroup = GetMessageGroup(messageCategory);

            return messageGroup != null ? messageGroup.Messages : new List<String>();
        }

        /// <summary>
        /// Get a message group for a specific message type
        /// </summary>
        /// <param name="messageCategory"></param>
        /// <returns></returns>
        public MessageGroup GetMessageGroup(MessageCategory messageCategory)
        {
            if (IsEmpty || !Contains(messageCategory)) return new MessageGroup(new List<string>(), messageCategory.ToTitle());

            return _messages[messageCategory];
        }

        /// <summary>
        /// Remove all messages
        /// </summary>
        public void Clear()
        {
            _messages.Clear();
        }

        /// <summary>
        /// Remove all message for a message type
        /// </summary>
        /// <param name="messageCategory"></param>
        public void Remove(MessageCategory messageCategory)
        {
            if (_messages.ContainsKey(messageCategory))
            {
                _messages.Remove(messageCategory);
            }
        }

        /// <summary>
        /// Remove a message description
        /// </summary>
        /// <param name="message"></param>
        public void Remove(String message)
        {
            foreach (var messageGroup in _messages.Values)
            {
                if (messageGroup.Messages.Contains(message))
                {
                    messageGroup.Messages.Remove(message);
                }
            }
        }

        #endregion

        #region IEnumerable<KeyValuePair<MessageCategory,MessageGroup>> Members

        public IEnumerator<KeyValuePair<MessageCategory, MessageGroup>> GetEnumerator()
        {
            return All.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return All.GetEnumerator();
        }

        #endregion

        #endregion

        #region Private Methods
        #endregion

    }
}
