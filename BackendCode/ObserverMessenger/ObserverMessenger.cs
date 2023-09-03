using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverMessenger
{

    // ObserverDelegate to define the delegate signature for methods
    public delegate void ObserverDelegate(object instance);

    // Singleton Observer class to manage subscriptions
    public class Observer
    {
        // Singleton instance of the Observer class
        private static Observer instance;

        // Public property to access the singleton instance
        public static Observer Instance => instance ??= new Observer();

        // Dictionary to store subscriptions by target type
        private readonly ConcurrentDictionary<Type, List<ObserverDelegate>> subscriptions;

        // Private constructor to create the singleton instance
        private Observer()
        {
            // Initialize subscriptions as a thread-safe ConcurrentDictionary
            subscriptions = new ConcurrentDictionary<Type, List<ObserverDelegate>>();
        }

        /// <summary>
        /// Subscribe a method to receive notifications for a specific target type.
        /// </summary>
        /// <param name="targetType">The type of object to observe.</param>
        /// <param name="method">The method to be called when notifications are sent.</param>
        public void Subscribe(Type targetType, ObserverDelegate method)
        {
            // Add or update the subscriptions dictionary for the given target type
            subscriptions.AddOrUpdate(targetType, new List<ObserverDelegate> { method }, (key, list) =>
            {
                list.Add(method);
                return list;
            });
        }

        /// <summary>
        /// Notify all subscribed methods with the provided instance.
        /// </summary>
        /// <param name="instance">The object instance to notify observers about.</param>
        public void Notify(object instance)
        {
            if (subscriptions.TryGetValue(instance.GetType(), out List<ObserverDelegate> methods))
            {
                // Invoke each subscribed method for the instance's type
                foreach (ObserverDelegate method in methods)
                {
                    method.Invoke(instance);
                }
            }
        }
    }

}
