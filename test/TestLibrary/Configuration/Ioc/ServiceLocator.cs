using System;
using Seterlund.CodeGuard;

namespace UIT.iDeal.TestLibrary.Configuration.Ioc
{
    public class ServiceLocator
    {
        private static Func<Type, object> resolve;
        private static Action<object> release;

        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public static object Resolve(Type type)
        {
            Guard.That(() => type)
                .IsTrue(x => x != null, "No resolve function has been set. Call IoC.SetResolveFunction first");
            return resolve(type);
        }

        public static void Release(object instance)
        {
            Guard.That(() => instance)
                .IsTrue(x => x != null, "No release action has been set. Call IoC.SetReleaseAction first");
            release(instance);
        }

        public static void SetResolveFunction(Func<Type, object> resolveFunction)
        {
            Guard.That(() => resolveFunction).IsNotNull();
            resolve = resolveFunction;
        }

        public static void SetReleaseAction(Action<object> releaseAction)
        {
            Guard.That(() => releaseAction).IsNotNull();
            release = releaseAction;
        }

        public static void Reset()
        {
            resolve = null;
            release = null;
        }
    }
}