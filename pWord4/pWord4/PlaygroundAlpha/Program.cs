using PlaygroundAlpha.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlaygroundAlpha
{
    class Program
    {
        private readonly HttpClient _httpClient = new HttpClient();

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            ////   This line will yield control to the UI as the request
            //// from the web service is happening.
            ////
            //// The UI thread is now free to perform other work.
            ////var stringData = await _httpClient.GetStringAsync(URL);
            ////TestEvent(stringData);

            //Counter2 counter = new Counter2(new Random().Next(20));
            //Console.WriteLine($"Threshold is {counter.Threshold}.");
            //counter.ThresholdReached +=  async(o,e) =>
            //{
            //    await Task.Run(() =>
            //    {
            //        Console.WriteLine("The threshold of {0} was reached at {1}.", e.Threshold, e.TimeReached);
            //        Environment.Exit(0);
            //    });
            //};

            //counter.APressed += Counter_APressed;

            //Console.WriteLine("press 'a' key to increase total");
            //while (Console.ReadKey(true).KeyChar == 'a')
            //{
            //    Console.WriteLine("adding one");
            //    counter.Add(1);
            //}

            CurriculumService svc = new CurriculumService();
            svc.DoRandom();
        }

        private static void Counter_APressed(object sender, EventArgs e)
        {
            Console.WriteLine($"Sender is of type: {sender.GetType().FullName}");
        }
    }

    class Counter
    {
        private int threshold;
        public int Threshold { get { return threshold; } }

        private int total;

        public Counter(int passedThreshold)
        {
            threshold = passedThreshold;
        }

        public void Add(int x)
        {
            total += x;
            if (total >= threshold)
            {
                OnThresholdReached((ThresholdReachedEventArgs)EventArgs.Empty);
            } else
            {
                OnThresholdNotReached((ThresholdReachedEventArgs)EventArgs.Empty);
            }
        }

        protected virtual void OnThresholdNotReached(ThresholdReachedEventArgs e)
        {
            EventHandler handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            EventHandler handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        protected virtual void OnAPressed(ThresholdReachedEventArgs e)
        {
            EventHandler handler = APressed;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        public event EventHandler ThresholdReached;
        public event EventHandler APressed;

    }



    class Counter2
    {
        private int threshold;
        public int Threshold { get { return threshold; } }

        private int total;

        public Counter2(int passedThreshold)
        {
            threshold = passedThreshold;
        }

        public void Add(int x)
        {
            total += x;
            if (total >= threshold)
            {
                var thea = new ThresholdReachedEventArgs();
                thea.Threshold = total;
                thea.TimeReached = DateTime.Now;
                OnThresholdReached(thea);
            }
        }

        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            ThresholdReachedEventHandler handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnAPressed(EventArgs e)
        {
            EventHandler handler = APressed;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        public event ThresholdReachedEventHandler ThresholdReached;
        public event EventHandler APressed;

    }


    public class ThresholdReachedEventArgs : EventArgs
    {
        public int Threshold { get; set; }
        public DateTime TimeReached { get; set; }
    }

    public delegate void ThresholdReachedEventHandler(object sender, ThresholdReachedEventArgs e);

}