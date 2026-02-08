=================================================
SIAP — REASONING AND EXECUTION CONTRACT
=================================================

This document defines the formal interaction contract between
conversational reasoning systems and SIAP execution infrastructure.

-------------------------------------------------
ROLE SEPARATION MODEL
-------------------------------------------------

Reasoning Layer:
Responsible for interpretation, planning, and proposal of changes.

Execution Layer (SIAP):
Responsible for structural validation, orchestration, and deterministic modification.

Reasoning systems MUST NOT directly perform structural project modifications without SIAP validation.

-------------------------------------------------
REASONING LAYER RESPONSIBILITIES
-------------------------------------------------

The reasoning layer is responsible for:

• Understanding user intent
• Analyzing project structure using SIAP-generated context
• Proposing incremental modifications
• Generating structured change plans
• Explaining modification rationale
• Respecting architectural boundaries

The reasoning layer MUST:

• Treat SIAP outputs as canonical project truth
• Avoid speculative restructuring
• Avoid generating uncontrolled bulk rewrites
• Always assume incomplete conversational context
• Prefer patch-based or task-based change proposals

-------------------------------------------------
EXECUTION LAYER RESPONSIBILITIES
-------------------------------------------------

SIAP execution layer is responsible for:

• Project inspection and indexing
• Change orchestration
• Structural validation
• Change sequencing
• Change traceability
• Rollback safety

SIAP MUST:

• Validate that modifications preserve project integrity
• Reject unsafe structural changes
• Ensure deterministic execution
• Maintain build and runtime stability
• Record modification history

-------------------------------------------------
CHANGE PROPOSAL PROTOCOL
-------------------------------------------------

All modifications must be expressed as structured proposals containing:

1. Intent Description
   What change is being proposed and why.

2. Impact Scope
   Files, modules, or architecture layers affected.

3. Dependency Awareness
   Identification of impacted services or interfaces.

4. Safety Assessment
   Risk analysis and rollback possibility.

5. Execution Plan
   Ordered list of modification steps.

-------------------------------------------------
FORBIDDEN REASONING BEHAVIOR
-------------------------------------------------

Reasoning systems MUST NOT:

• Modify multiple unrelated modules simultaneously
• Rewrite core architecture without explicit authorization
• Generate changes that cannot be validated incrementally
• Assume file structure without SIAP inspection
• Perform silent structural drift

-------------------------------------------------
SAFE EXECUTION GUARANTEES
-------------------------------------------------

SIAP guarantees that:

• Every accepted modification is reproducible
• Execution order is deterministic
• Structural violations are prevented
• Rollback is possible whenever feasible

-------------------------------------------------
CONTINUITY PRESERVATION RULE
-------------------------------------------------

All modifications must preserve project continuity.

Continuity is defined as:

• The project remains buildable
• The project remains runnable
• Existing features remain functional unless explicitly replaced

-------------------------------------------------
SESSION LIMITATION MITIGATION
-------------------------------------------------

Reasoning systems must acknowledge that:

• Conversational memory is temporary
• Structural memory must be reconstructed using SIAP inspection outputs
• No architectural assumptions should be made across sessions

-------------------------------------------------
TASK ORCHESTRATION PRINCIPLE
-------------------------------------------------

Large modifications must be decomposed into atomic tasks.

Each task must:

• Have a clear completion condition
• Be independently verifiable
• Preserve project stability

-------------------------------------------------
ARCHITECTURAL AUTHORITY MODEL
-------------------------------------------------

Architecture is defined by SIAP documentation and inspection output.

Reasoning systems may propose evolution but cannot redefine architecture silently.

-------------------------------------------------
ERROR HANDLING PROTOCOL
-------------------------------------------------

If a proposed modification introduces structural uncertainty:

• Execution must be halted
• Validation must be requested
• Alternative safe proposals should be generated

-------------------------------------------------
ETHICAL ENGINEERING PRINCIPLE
-------------------------------------------------

Speed must never override integrity.

Convenience must never override traceability.

Automation must never override determinism.

=================================================
END OF CONTRACT
=================================================
