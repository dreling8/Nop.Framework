using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Domain.Test;

namespace Nop.Services.Tests
{
    /// <summary>
    /// Test service
    /// </summary>
    public partial class TestService : ITestService
    {
        #region Constants


        #endregion

        #region Fields

        private readonly IRepository<TestEntity> _testRepository;

        #endregion

        #region Ctor

        public TestService(IRepository<TestEntity> testRepository)
        {
            this._testRepository = testRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all tests
        /// </summary> 
        /// <returns>Tests</returns>
        public virtual IList<TestEntity> GetAllTests()
        {
            return _testRepository.Table.Where(p => p.Name != null).ToList();
        }

        /// <summary>
        /// Gets a topic
        /// </summary>
        /// <param name="testId">The test identifier</param>
        /// <returns>Test</returns>
        public virtual TestEntity GetTestById(int testId)
        {
            if (testId == 0)
                return null;
            return _testRepository.GetById(testId);
        }

        /// <summary>
        /// Inserts a test
        /// </summary>
        /// <param name="test">Test</param>
        public virtual void InsertTest(TestEntity test)
        {
            if (test == null)
                throw new ArgumentNullException("test");
            _testRepository.Insert(test);

        }

        /// <summary>
        /// Updates the test
        /// </summary>
        /// <param name="test">Test</param>
        public virtual void UpdateTest(TestEntity test)
        {
            if (test == null)
                throw new ArgumentNullException("test");
            _testRepository.Update(test);
        }

        /// <summary>
        /// Deletes a test
        /// </summary>
        /// <param name="test">Test</param>
        public virtual void DeleteTest(TestEntity test)
        {
            if (test == null)
                throw new ArgumentNullException("test");
            _testRepository.Delete(test);
        }
           
        #endregion
    }
}
