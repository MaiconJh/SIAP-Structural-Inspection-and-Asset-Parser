=================================================
SIAP — STRUCTURAL SEMANTIC EXPANSION GUIDE
=================================================

This document defines the semantic structure of the SIAP repository and establishes rules for how the project must evolve structurally.

Its purpose is to ensure consistency, predictability, and maintainability as the project expands.

-------------------------------------------------
STRUCTURE PHILOSOPHY
-------------------------------------------------

The repository structure represents architecture, not convenience.

Folders and modules must reflect engineering responsibilities.

Structural organization must always communicate semantic meaning.

-------------------------------------------------
PRIMARY STRUCTURE PRINCIPLES
-------------------------------------------------

1. Single Responsibility by Module
Each directory must represent one architectural responsibility.

2. Layer Separation
UI, Core, Plugins, Tasks, and Infrastructure must remain logically separated.

3. Predictable Growth
New features must extend existing structure rather than reorganize it.

4. Structural Stability
Folder hierarchy must remain stable unless explicitly redesigned through governance.

-------------------------------------------------
STRUCTURE EXPANSION RULE
-------------------------------------------------

When adding new functionality:

You must:

• Locate the correct architectural layer
• Extend existing modules when possible
• Create new modules only when responsibility is clearly isolated
• Preserve existing directory semantics

-------------------------------------------------
FOLDER SEMANTICS
-------------------------------------------------

Root Directory
Represents the product boundary and global orchestration.

docs/
Contains governance, contracts, architecture definitions, and structural documentation.
This directory is authoritative for project rules.

src/
Contains implementation code organized by architecture layers.

plugins/
Contains extensible modules that expand functionality without modifying core architecture.

tasks/
Contains automation workflows and execution orchestration logic.

infrastructure/
Contains cross-cutting services such as configuration, logging, and integration.

-------------------------------------------------
CODE ORGANIZATION RULE
-------------------------------------------------

Modules must be organized according to responsibility, not technical similarity.

Example:

Correct:
Scanner logic grouped under scanning responsibility.

Incorrect:
Grouping files only by file type or language feature.

-------------------------------------------------
NEW MODULE CREATION RULE
-------------------------------------------------

A new module may only be created when:

• Responsibility cannot logically belong to an existing module
• The feature introduces a new architectural boundary
• The module can be independently reasoned about

-------------------------------------------------
MODULE EXPANSION RULE
-------------------------------------------------

Existing modules must be expanded before new modules are created, unless doing so would violate single responsibility.

-------------------------------------------------
DEPENDENCY DIRECTION RULE
-------------------------------------------------

Dependencies must follow architectural flow.

UI → Core → Infrastructure

Plugins → Core

Tasks → Core and Infrastructure

Core must never depend on Plugins or UI.

-------------------------------------------------
NAMING SEMANTICS RULE
-------------------------------------------------

Directory and module names must:

• Represent functional responsibility
• Avoid vague or generic terms
• Avoid implementation-specific names

-------------------------------------------------
FILE PLACEMENT RULE
-------------------------------------------------

New files must be placed where their responsibility naturally belongs.

Files must never be placed in directories for convenience or proximity.

-------------------------------------------------
STRUCTURAL MODIFICATION RULE
-------------------------------------------------

Reorganizing directories is considered architectural change.

Such changes must:

• Be explicitly proposed
• Be validated against governance documents
• Provide migration strategy

-------------------------------------------------
PLUGIN STRUCTURE RULE
-------------------------------------------------

Plugins must remain isolated from core implementation.

Plugins may:

• Extend features
• Register new tasks
• Provide additional analysis modules

Plugins must not:

• Modify core services directly
• Introduce dependency loops
• Override architecture contracts

-------------------------------------------------
TASK STRUCTURE RULE
-------------------------------------------------

Tasks must represent deterministic execution workflows.

Tasks must:

• Operate using core services
• Remain isolated from UI logic
• Follow lifecycle contracts

-------------------------------------------------
DOCUMENTATION SYNCHRONIZATION RULE
-------------------------------------------------

When structural changes occur:

Corresponding updates must be made to /docs.

-------------------------------------------------
ANTI-PATTERN PREVENTION
-------------------------------------------------

The following structural behaviors are forbidden:

• Convenience folder creation
• Mixing responsibilities in single modules
• Cross-layer dependency shortcuts
• Silent architecture reorganization

-------------------------------------------------
EXPANSION VALIDATION CHECKLIST
-------------------------------------------------

Before creating new structure, confirm:

• Does this belong to an existing module?
• Does this violate layer separation?
• Does this preserve naming semantics?
• Does this maintain dependency direction?
• Does this preserve structural clarity?

-------------------------------------------------
STRUCTURAL INTEGRITY ETHOS
-------------------------------------------------

Structure communicates architecture.

Architecture communicates intent.

Intent must remain visible through repository organization.

=================================================
END OF STRUCTURAL GUIDE
=================================================
