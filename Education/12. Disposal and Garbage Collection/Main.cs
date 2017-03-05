using System;
using System.Collections.Generic;
using System.Text;

namespace Education._12._Disposal_and_Garbage_Collection
{
    class Main : IExecutable
    {
        public void Execute()
        {
            WeakReferences();

            GC.Collect();

            WeakReference2();
        }

        private void WeakReference2()
        {
            Widget.ListAllWidgets();
        }

        private void WeakReferences()
        {
            var weak = new WeakReference(new StringBuilder("this is a test"));
            //Console.WriteLine(weak.Target);
            //GC.Collect();
            //Console.WriteLine(weak.Target);

            //var sb = weak.Target;
            //if (sb != null)
            //{
            //    Console.WriteLine(sb);
            //}

            var widget1 = new Widget("First One");
            var widget2 = new Widget("Second One");

            Widget.ListAllWidgets();

        }
    }

    class Widget
    {
        static List<WeakReference> allWidgets = new List<WeakReference>();

        public readonly string Name;

        public Widget(string name)
        {
            Name = name;
            allWidgets.Add(new WeakReference(this));
        }

        public static void ListAllWidgets()
        {
            foreach (var widgetRef in allWidgets)
            {
                var widget = (Widget)widgetRef.Target;
                if (widget != null)
                {
                    Console.WriteLine(widget.Name);
                }
            }
        }
    }
}