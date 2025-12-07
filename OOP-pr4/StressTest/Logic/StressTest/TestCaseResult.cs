namespace StressTest
{
    public struct TestCaseResult
    {
        /// <summary>
        /// Test result (enumeration type)
        /// </summary>
        public TestResult Result;
        /// <summary>
        /// Description of reason for failure
        /// </summary>
        public string ReasonForFailure;
        public TestCaseResult(TestResult result, string reason)
        {
            Result = result;
            ReasonForFailure = reason;
        }
    }
}