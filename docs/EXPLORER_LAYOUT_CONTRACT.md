=================================================
SIAP — EXPLORER LAYOUT AND FILE BROWSER CONTRACT
=================================================

This document defines the mandatory layout, resizing model,
and file-browsing interaction standards for SIAP desktop UI.

It formalizes a GitHub-like repository browsing experience:
sidebar navigation + path header + directory listing panels,
with flexible resizing and deterministic interaction behavior.

-------------------------------------------------
AUTHORITY AND DEPENDENCIES
-------------------------------------------------

This contract is subordinate to:

• INTERFACE_DESIGN_CONTRACT.md
• VISUAL_FEEDBACK_CONTRACT.md
• VISUAL_COMPONENT_EXECUTION_CONTRACT.md
• VISUAL_TOKEN_REGISTRY_SPEC.md

If conflicts exist, the higher authority contracts override.

-------------------------------------------------
PRIMARY EXPERIENCE GOAL
-------------------------------------------------

The Explorer must feel similar to GitHub’s repository file view:

• Clear directory hierarchy
• Path breadcrumbs
• Contextual toolbar per directory
• Clean listing of folders and files
• Strong information density without clutter
• Resizable areas with predictable behavior

This must be achieved while maintaining SIAP’s unique identity.

-------------------------------------------------
MANDATORY LAYOUT COMPOSITION
-------------------------------------------------

The application must support a split-based layout:

Left Panel (Navigation / Explorer)
Right Panel (Content / Logs / Preview)

The core UI must allow resizing between panels.

The minimum supported layout:

Root Layout
 ├ Top Toolbar (global actions)
 ├ Explorer Header (project + breadcrumbs + directory actions)
 ├ Split Content Region
 │   ├ Left: Explorer Tree / Directory Navigation
 │   └ Right: Log / Preview / Details
 └ Status Bar (persistent state summary)

-------------------------------------------------
RESIZING AND SPLITTER MODEL
-------------------------------------------------

The interface MUST support user-driven resizing.

Rules:

• Splitters must resize adjacent panels deterministically
• Splitters must visually communicate interactivity
• Panels must use flexible sizing (star sizing), not fixed widths
• Panels must enforce minimum width limits to prevent collapse

Splitter requirements:

• Cursor change on hover (resize cursor)
• Hover highlight feedback
• Optional “preview resize” behavior allowed
• Must not break layout when window resizes

-------------------------------------------------
EXPLORER HEADER MODEL (GITHUB-LIKE)
-------------------------------------------------

Explorer must include a header above the listing area.

Header must display:

1) Project Identity
   • Project name (folder/repo name)
   • Root path indicator

2) Breadcrumb Path
   • Example: Project / plugins / example / pages
   • Each segment must be clickable (jump to that folder)

3) Directory Context Actions
   • Rescan
   • Select All / Clear
   • Optional: Copy Path, Reveal in Explorer

Header must remain visible and stable during content scrolling.

-------------------------------------------------
DIRECTORY LISTING MODEL
-------------------------------------------------

The file view must support a directory listing mode similar to GitHub:

Listing row fields:

• Icon (folder/file)
• Name
• Optional metadata (size / modified / type) - phase gated
• Selection control (checkbox)

Directory listing rules:

• Folders must appear before files
• Alphabetical sort (case-insensitive)
• Row height must be consistent
• Large directories must remain scrollable and performant

-------------------------------------------------
TREE + LIST HYBRID REQUIREMENT
-------------------------------------------------

SIAP may provide both:

• Tree View (hierarchical navigation)
• Directory Listing View (current folder contents)

If both are present:

• Tree determines current folder
• Listing shows contents of selected folder
• Breadcrumb reflects the selected folder

The system must not duplicate logic inconsistently.

-------------------------------------------------
SELECTION MODEL
-------------------------------------------------

Selection must support tri-state logic where applicable.

Selection behaviors:

• Selecting a folder selects its descendants (configurable by mode)
• Selecting individual files updates parent state (partial selection)

Selection state must persist across:

• Filtering
• Rescans (when possible)
• Panel resize
• View switches

-------------------------------------------------
FILTERING MODEL
-------------------------------------------------

Filtering must behave like GitHub search within a view scope.

Rules:

• Filter affects visibility only, not underlying selection state
• Filter must be fast and incremental
• Clear action must be available
• Filtering should default to current folder scope
  (global scope is optional and must be explicit)

-------------------------------------------------
VISUAL FEEDBACK REQUIREMENTS
-------------------------------------------------

All Explorer interactions must comply with VISUAL_FEEDBACK_CONTRACT.

Mandatory feedback:

• Hover highlight on rows
• Press feedback on rows and actions
• Focus outline on keyboard navigation
• Selection highlight persistence
• Clear disabled states for unavailable actions
• Loading indicators during scan/rescan

-------------------------------------------------
DENSITY AND READABILITY RULE
-------------------------------------------------

Explorer must achieve GitHub-like information density.

Rules:

• Avoid oversized cards inside listing area
• Use subtle separators instead of heavy borders
• Maintain consistent spacing tokens
• Avoid empty “white zones” or dead areas
• Content must align to a clear grid

-------------------------------------------------
RESPONSIVE BEHAVIOR
-------------------------------------------------

When window width decreases:

• Explorer panel may shrink until minimum width
• Log/Preview panel may shrink until minimum width
• Content must remain usable at minimum supported width

No content area may disappear silently.

If collapse mode is implemented, it must be explicit.

-------------------------------------------------
IMPLEMENTATION AUTHORITY (EXECUTION)
-------------------------------------------------

UI implementation must be performed using registered components
and token-driven styling.

Hardcoded colors, spacing, and timings are forbidden.

All layout modules must be compatible with:
• VISUAL_COMPONENT_EXECUTION_CONTRACT
• VISUAL_TOKEN_REGISTRY_SPEC

-------------------------------------------------
FORBIDDEN BEHAVIOR
-------------------------------------------------

The following are prohibited:

• Fixed-width panels without justification
• Splitters that do not work or do not show resize cursor
• Header elements that scroll away unintentionally
• Inconsistent sorting between views
• Separate selection models per view
• Visual dead zones caused by unstyled root containers

-------------------------------------------------
VALIDATION CHECKLIST
-------------------------------------------------

The Explorer is considered compliant only if:

1) Panels can be resized smoothly via splitters
2) Breadcrumb path updates correctly and segments are clickable
3) Directory listing displays folders first, then files, sorted
4) Selection state is preserved across filter + resize
5) UI remains readable and functional at minimum supported width
6) Feedback behaviors exist for hover/press/focus/loading

-------------------------------------------------
FINAL PRINCIPLE
-------------------------------------------------

The Explorer must feel:

• GitHub-like in structure
• IDE-like in usability
• SIAP-like in identity
• Deterministic in behavior
• Expandable by contract

=================================================
END OF CONTRACT
=================================================
