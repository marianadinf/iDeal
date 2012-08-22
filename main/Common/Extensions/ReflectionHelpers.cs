using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace UIT.iDeal.Common.Extensions
{
    public static class ReflectionHelpers
    {
        public static IEnumerable<Type> GetControllers<T>()
        {
            return from c in typeof(T).Assembly.GetExportedTypes()
                   where !c.IsAbstract &&
                       typeof(Controller).IsAssignableFrom(c)
                   select c;
        }

        /// <summary>
        /// Converts an expression into a <see cref="MemberInfo"/>.
        /// </summary>
        /// <param name="expression">The expression to convert.</param>
        /// <returns>The member info.</returns>
        public static MemberInfo GetMemberInfo(this Expression expression)
        {
            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
                memberExpression = (MemberExpression)lambda.Body;

            return memberExpression.Member;
        }

        public static Assembly GetAssemblyNamed(string assemblyName)
        {
            Debug.Assert(string.IsNullOrEmpty(assemblyName) == false);

            try
            {
                Assembly assembly;
                if (IsAssemblyFile(assemblyName))
                {
                    if (Path.GetDirectoryName(assemblyName) == AppDomain.CurrentDomain.BaseDirectory)
                    {
                        assembly = Assembly.Load(Path.GetFileNameWithoutExtension(assemblyName));
                    }
                    else
                    {
                        assembly = Assembly.LoadFile(assemblyName);
                    }
                }
                else
                {
                    assembly = Assembly.Load(assemblyName);
                }
                return assembly;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (FileLoadException)
            {
                throw;
            }
            catch (BadImageFormatException)
            {
                throw;
            }
            catch (Exception e)
            {
                // in theory there should be no other exception kind
                throw new Exception(string.Format("Could not load assembly {0}", assemblyName), e);
            }
        }

        public static bool IsAssemblyFile(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }

            string extension;
            try
            {
                extension = Path.GetExtension(filePath);
            }
            catch (ArgumentException)
            {
                // path contains invalid characters...
                return false;
            }
            return IsDll(extension) || IsExe(extension);
        }

        private static bool IsDll(string extension)
        {
            return ".dll".Equals(extension, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsExe(string extension)
        {
            return ".exe".Equals(extension, StringComparison.OrdinalIgnoreCase);
        }

    }
}
