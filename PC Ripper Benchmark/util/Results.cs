using System;
using System.Collections.Generic;

namespace PC_Ripper_Benchmark.util {
    public abstract class Results {

        /// <summary>
        /// Represents a dictionary containing the name of the test,
        /// and the average time it took the test.
        /// </summary>
        public abstract Tuple<string, TimeSpan> AverageTest { get; }

        /// <summary>
        /// Represents the individual time for each test before the averaging.
        /// </summary>
        public abstract Dictionary<string, TimeSpan> TestsCollection { get; }

        /// <summary>
        /// Represents the score for this particular test.
        /// </summary>
        public abstract byte Score { get; }

        /// <summary>
        /// Represents the description for this particular test.
        /// </summary>
        public abstract string Description { get; }

        protected abstract void GenerateDescription();

        protected abstract void GenerateScore();

        /// <summary>
        /// Adds a particular test to the <see cref="TestsCollection"/>.
        /// </summary>
        /// <param name="name">The name of the function and test #.</param>
        /// <param name="duration">A <see cref="TimeSpan"/> that represents
        /// a duration for the test.</param>
        
        protected virtual void AddTest(string name, TimeSpan duration) {
            TestsCollection.Add(name, duration);
        }

        protected virtual Tuple<string, TimeSpan> GenerateAverageTest(Dictionary<string, TimeSpan> testCollection) {
            var newCollection = Tuple.Create(testCollection.Keys[], new TimeSpan());
            int count = testCollection.Count;

            foreach (KeyValuePair<string, TimeSpan> kvp in testCollection) {
                
            }

            return newCollection;
        }

    }
}
