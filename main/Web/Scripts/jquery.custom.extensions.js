// Replace the normal jQuery getScript function with one that supports
// debugging and which references the script files as external resources
// rather than inline.
jQuery.extend({
   getScript: function(url, callback) {
      
      var head = document.getElementsByTagName("head")[0];
      var script = document.createElement("script");
      script.src = url;
      script.type = "text/javascript";

      // Handle Script loading
      {
         var done = false;

         // Attach handlers for all browsers
         script.onload = script.onreadystatechange = function(){
            if ( !done && (!this.readyState ||
                  this.readyState == "loaded" || this.readyState == "complete") ) {
               done = true;
               if (callback)
                  callback();

               // Handle memory leak in IE
               script.onload = script.onreadystatechange = null;
            }
         };
      }

      head.appendChild(script);

      // We handle everything using the script element injection
      return undefined;
   }

});

String.prototype.camelCase = function () {
    var s = trim(this);
    return (/\S[A-Z]/.test(s)) ?
  s.replace(/(.)([A-Z])/g, function (t, a, b) { return a + ' ' + b.toLowerCase(); }) :
  s.replace(/( )([a-z])/g, function (t, a, b) { return b.toUpperCase(); });
};

$.fn.outerHtml = function () {

    if (!this || !this[0]) return "";

    var result = $(this[0]).wrap("<div/>").parent().html();

    return result;
};

$.fn.propertyExists = function (propertyName) {
    if (this == undefined || this[0] == undefined || typeof propertyName != "string") return false;

    var object = this[0];

    return object[propertyName] !== undefined;
};

$.fn.resolve = function (propertyName, defaultValue, defaultValueFunctionArgs) {

    var objectIsDefined = this !== undefined && this[0] != undefined,
        object = this[0];

    //if no property name is defined return undefined
    if (propertyName == undefined || propertyName == null) return undefined;
    
    //if the objects being resolved is  defined and the property exist return the property's value
    if (objectIsDefined && $(object).propertyExists(propertyName)) return object[propertyName];

    //if any default value use it as returned value
    if (defaultValue) {
        return typeof defaultValue == "function" ? defaultValue(defaultValueFunctionArgs) : defaultValue;
    } 
    // otherwise try to construct an instance capitalising the first letter of the property name
    else {
        try {
            var objectName = propertyName.substr(0, 1).toUpperCase() + propertyName.substr(1);
            return new window[objectName](defaultValueFunctionArgs);

        } catch (e) {
            return undefined;
        }
    }

};

(function ($) {
    $.strRemove = function (theTarget, theString) {
        return $("<div/>").append(
            $(theTarget, theString).remove().end()
        ).html();
    };
})(jQuery);
