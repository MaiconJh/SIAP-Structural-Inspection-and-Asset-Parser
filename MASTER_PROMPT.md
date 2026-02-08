=================================================
SIAP — MASTER DEVELOPMENT GOVERNANCE PROMPT
=================================================

This file is the central orientation and governance entry point for development inside the SIAP repository.

All contributors and automated systems must follow the rules and contracts defined in the /docs directory.

-------------------------------------------------
AUTHORITATIVE GOVERNANCE DOCUMENTS
-------------------------------------------------

The following documents define how the project must be developed, expanded, and maintained:

docs/PROJECT_SOUL.md
docs/EXECUTION_CONTRACT.md
docs/PATCH_SPECIFICATION.md
docs/STRUCTURAL_VALIDATION.md
docs/TASK_LIFECYCLE.md
docs/STRUCTURE_GUIDE.md
docs/REPOSITORY_GROWTH_STRATEGY.md
docs/GOVERNANCE_PROMPT.md

These documents are considered engineering contracts and must be treated as authoritative.

-------------------------------------------------
DOCUMENT RESPONSIBILITY AND USAGE
-------------------------------------------------

PROJECT_SOUL.md
Defines the philosophical and engineering identity of the project.
Use this document to understand:

• Long-term purpose
• Engineering ethos
• Safe development mindset
• Project direction

---

EXECUTION_CONTRACT.md
Defines how reasoning and implementation must interact.

Use this document when:

• Planning implementation
• Defining modification workflows
• Ensuring change governance compliance

---

PATCH_SPECIFICATION.md
Defines how code and structural modifications must be formatted and executed.

Use this document when:

• Creating modifications
• Structuring changes
• Planning incremental evolution
• Defining rollback safety

---

STRUCTURAL_VALIDATION.md
Defines integrity and architecture safety rules.

Use this document when:

• Validating modifications
• Ensuring architecture stability
• Confirming dependency safety
• Verifying build integrity

---

TASK_LIFECYCLE.md
Defines how work must be broken into execution tasks.

Use this document when:

• Implementing multi-step features
• Sequencing complex changes
• Tracking execution completion
• Handling failures and recovery

---

STRUCTURE_GUIDE.md
Defines semantic repository structure and module responsibilities.

Use this document when:

• Adding new files or modules
• Expanding architecture
• Organizing directories
• Determining correct placement of new code

---

REPOSITORY_GROWTH_STRATEGY.md
Defines long-term repository evolution and expansion phases.

Use this document when:

• Introducing major capabilities
• Planning architectural expansion
• Deciding module maturity transitions
• Evaluating refactor necessity

---

GOVERNANCE_PROMPT.md
Defines behavioral development governance for contributors and automated systems.

Use this document when:

• Determining development discipline
• Ensuring compliance with safe engineering practices
• Validating implementation workflow behavior

-------------------------------------------------
MANDATORY DOCUMENT USAGE ORDER
-------------------------------------------------

When performing development, follow this reading and decision order:

1. PROJECT_SOUL.md
   Understand intent and philosophy.

2. STRUCTURE_GUIDE.md
   Identify where changes belong structurally.

3. EXECUTION_CONTRACT.md
   Define how implementation must be performed.

4. PATCH_SPECIFICATION.md
   Structure modifications.

5. TASK_LIFECYCLE.md
   Break changes into executable units.

6. STRUCTURAL_VALIDATION.md
   Verify architecture and runtime integrity.

7. REPOSITORY_GROWTH_STRATEGY.md
   Confirm expansion aligns with long-term evolution.

8. GOVERNANCE_PROMPT.md
   Validate behavioral compliance during execution.

-------------------------------------------------
PRIMARY DEVELOPMENT PRINCIPLES
-------------------------------------------------

• The repository structure represents architecture.
• Architecture must remain visible through organization.
• Changes must be incremental and traceable.
• Project must remain buildable and runnable at all times.
• Governance documents override implementation convenience.

-------------------------------------------------
STRICT IMPLEMENTATION LIMITS
-------------------------------------------------

A single implementation cycle must NOT:

• Modify more than 5 files
• Introduce more than 2 new files
• Perform architectural restructuring
• Introduce cross-layer dependencies
• Perform opportunistic refactoring

If limits are exceeded:
Split work into multiple tasks.

-------------------------------------------------
ROLLBACK AND SAFETY REQUIREMENTS
-------------------------------------------------

Every modification must include:

• Rollback plan
• Validation checklist
• Risk explanation
• Dependency awareness

-------------------------------------------------
UNCERTAINTY HANDLING RULE
-------------------------------------------------

If repository structure or documentation is unclear:

Stop implementation.
Request clarification.
Never assume.

-------------------------------------------------
PROTOCOL GOVERNANCE RULE
-------------------------------------------------

Governance documents inside /docs are stable contracts.

They must NOT be modified automatically.

New protocols or modifications must:

1. Be proposed explicitly
2. Include impact analysis
3. Preserve compatibility
4. Receive approval
5. Be version tracked

All proposals must be created inside:

docs/proposals/

-------------------------------------------------
BUILD AND RUNTIME PROTECTION RULE
-------------------------------------------------

The project must remain:

• Buildable
• Runnable
• Structurally valid

After every modification.

If not possible:
Implementation must be redesigned into safe phases.

-------------------------------------------------
MANDATORY RESPONSE STRUCTURE
-------------------------------------------------

Every development response must include:

1. Request Understanding
2. Documents Referenced
3. Constraints Identified
4. Task Breakdown
5. Patch Scope
6. Rollback Plan
7. Validation Checklist
8. Risk Assessment

-------------------------------------------------
ENGINEERING ETHOS
-------------------------------------------------

Consistency over speed.
Safety over convenience.
Incremental evolution over large rewrites.
Architecture preservation over improvisation.
Deterministic execution over speculation.

=================================================
END OF MASTER GOVERNANCE PROMPT
=================================================
