using System.Collections.Generic;
using Nop.Domain.Test;

namespace Nop.Services.Tests
{
    /// <summary>
    /// Test service interface
    /// </summary>
    public partial interface ITestService
    { 
        /// <summary>
        /// Gets all tests
        /// </summary> 
        /// <returns>Tests</returns>
        IList<TestEntity> GetAllTests();

        /// <summary>
        /// Gets a test
        /// </summary>
        /// <param name="testId">The test identifier</param>
        /// <returns>Test</returns>
        TestEntity GetTestById(int testId);
         
        /// <summary>
        /// Inserts a test
        /// </summary>
        /// <param name="test">Test</param>
        void InsertTest(TestEntity test);
         
        /// <summary>
        /// Updates the test
        /// </summary>
        /// <param name="test">Test</param>
        void UpdateTest(TestEntity test);

        /// <summary>
        /// Deletes a test
        /// </summary>
        /// <param name="test">Test</param>
        void DeleteTest(TestEntity test);
        
    }
}
