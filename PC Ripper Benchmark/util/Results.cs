using System;
using System.Collections.Generic;

namespace PC_Ripper_Benchmark.util {

    /// <summary>
    /// The abstract <see cref="Results"/> class.
    /// <para>Contains property signatures, and method signatures for
    /// similar data for a particular component results.</para>
    /// <para>Author: Anthony Jaghab (c), all rights reserved.</para>
    /// </summary>

    public abstract class Results {

        #region Abstract properties for a given component.

        /// <summary>
        /// Represents a <see cref="Tuple{T1, T2}"/> containing the name of the test,
        /// and the average time it took the test.
        /// </summary>
        public abstract Tuple<string, TimeSpan> AverageTest { get; }

        /// <summary>
        /// Represents the individual time for each test before the averaging.
        /// </summary>
        public abstract List<TimeSpan> TestCollection { get; }

        /// <summary>
        /// Represents the score for this particular test.
        /// </summary>
        public abstract byte Score { get; }

        /// <summary>
        /// Represents the description for this particular test.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Represents the test name for this component.
        /// </summary>
        public abstract string TestName { get; }

        /// <summary>
        /// Gets the number of unique individual tests
        /// for the component.
        /// </summary>
        public abstract byte UniqueTestCount { get; }

        #endregion

        #region Abstract methods.

        /// <summary>
        /// Generates a particular description based on a
        /// given component test.
        /// </summary>

        protected abstract void GenerateDescription();

        /// <summary>
        /// Generates a score for a particular component.
        /// </summary>

        protected abstract byte GenerateScore();

        /// <summary>
        /// Adds a particular test to the <see cref="TestCollection"/>.
        /// </summary>
        /// <param name="duration">A <see cref="TimeSpan"/> that represents
        /// a duration for the test.</param>

        protected virtual void AddTest(TimeSpan duration) {
            this.TestCollection.Add(duration);
        }

        /// <summary>
        /// Generates a <see cref="Tuple{T1, T2}"/> containing 
        /// the Name and average test.
        /// </summary>
        /// <param name="testCollection"></param>
        /// <returns>A Tuple containing a </returns>

        protected virtual Tuple<string, TimeSpan> GenerateAverageTest(List<TimeSpan> testCollection) {
            TimeSpan totalTime = new TimeSpan();

            foreach (TimeSpan time in testCollection) {
                totalTime = totalTime.Add(time);
            }

            if (testCollection.Count > 0) {
                return Tuple.Create(this.TestName, new TimeSpan(totalTime.Ticks / testCollection.Count));
            } else {
                return Tuple.Create(this.TestName, new TimeSpan(totalTime.Ticks / 1));
            }


        }

        #endregion
    }
}
