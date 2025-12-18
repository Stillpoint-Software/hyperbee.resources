using Microsoft.VisualStudio.TestTools.UnitTesting;

// Explicitly enable test parallelization at method level
// This allows multiple test methods to run in parallel for faster execution
// Safe for resource loading tests that don't share mutable state
[assembly: Parallelize( Workers = 0, Scope = ExecutionScope.MethodLevel )]
