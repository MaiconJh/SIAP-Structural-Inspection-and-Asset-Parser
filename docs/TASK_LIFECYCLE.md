=================================================
SIAP — TASK LIFECYCLE CONTRACT
=================================================

This document defines the lifecycle model for automated engineering tasks.

-------------------------------------------------
TASK PURPOSE
-------------------------------------------------

Tasks represent deterministic execution units responsible for performing project operations safely.

-------------------------------------------------
TASK STATES
-------------------------------------------------

CREATED
Task definition exists but is not scheduled.

QUEUED
Task is waiting execution.

RUNNING
Task is actively executing operations.

VALIDATING
Task results are being verified.

COMPLETED
Task executed successfully.

FAILED
Task execution failed validation or runtime.

ROLLED_BACK
Task changes were reverted.

-------------------------------------------------
TASK STRUCTURE
-------------------------------------------------

Each task must define:

TASK_METADATA
- Task ID
- Description
- Priority
- Dependencies

TASK_OPERATIONS
- Ordered list of patch operations

TASK_VALIDATION
- Completion verification rules

TASK_ROLLBACK
- Reversal strategy

-------------------------------------------------
TASK EXECUTION RULES
-------------------------------------------------

Tasks must:

• Execute sequentially unless explicitly parallel-safe
• Report progress deterministically
• Validate results before completion

-------------------------------------------------
DEPENDENCY RULE
-------------------------------------------------

Tasks must not execute until dependencies are completed successfully.

-------------------------------------------------
FAILURE RULE
-------------------------------------------------

If a task fails:

• Execution must stop
• Rollback must be evaluated
• Failure must be logged

-------------------------------------------------
TASK TRACEABILITY
-------------------------------------------------

Every task must be recorded for audit and reproducibility.

=================================================
END OF CONTRACT
=================================================
