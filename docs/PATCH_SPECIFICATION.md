=================================================
SIAP — PATCH SPECIFICATION CONTRACT
=================================================

This document defines the universal format and rules for describing project modifications.

-------------------------------------------------
PATCH PHILOSOPHY
-------------------------------------------------

All modifications must be expressed as structured patches.

A patch represents a deterministic, traceable, and reversible project modification.

Patches must never be implicit or informal.

-------------------------------------------------
PATCH STRUCTURE
-------------------------------------------------

Each patch must contain:

PATCH_METADATA
- Patch ID
- Author or Source
- Timestamp
- Version compatibility

PATCH_INTENT
- Human-readable description of purpose
- Reasoning behind the change

PATCH_SCOPE
- List of affected files or modules
- Classification:
  CREATE
  MODIFY
  DELETE
  MOVE

PATCH_DEPENDENCIES
- Services or modules affected indirectly
- Required execution order

PATCH_OPERATIONS
- Ordered list of atomic changes
- Each operation must be independently verifiable

PATCH_VALIDATION
- Expected results
- Test conditions
- Structural validation rules

PATCH_ROLLBACK
- Strategy to revert changes safely

-------------------------------------------------
ATOMIC CHANGE PRINCIPLE
-------------------------------------------------

Each patch operation must:

• Modify only one structural responsibility
• Be reversible
• Be verifiable

-------------------------------------------------
PATCH SAFETY RULES
-------------------------------------------------

Patches must never:

• Introduce undocumented dependencies
• Modify architecture silently
• Affect unrelated modules
• Contain ambiguous instructions

-------------------------------------------------
PATCH EXECUTION RULE
-------------------------------------------------

Patches must be executed sequentially and validated after each step.

=================================================
END OF CONTRACT
=================================================
