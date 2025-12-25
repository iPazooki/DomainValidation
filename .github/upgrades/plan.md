# .NET 10 Upgrade Plan for `DomainValidation`

## Table of Contents
- [1. Executive Summary](#1-executive-summary)
- [2. Migration Strategy](#2-migration-strategy)
- [3. Detailed Dependency Analysis](#3-detailed-dependency-analysis)
- [4. Project-by-Project Plans](#4-project-by-project-plans)
- [5. Package Update Reference](#5-package-update-reference)
- [6. Breaking Changes Catalog](#6-breaking-changes-catalog)
- [7. Testing & Validation Strategy](#7-testing--validation-strategy)
- [8. Complexity & Effort Assessment](#8-complexity--effort-assessment)
- [9. Risk Management](#9-risk-management)
- [10. Source Control Strategy](#10-source-control-strategy)
- [11. Success Criteria](#11-success-criteria)

---

## 1. Executive Summary

### Selected Strategy
**All-At-Once Strategy** – All projects upgraded simultaneously in a single coordinated operation.

**Rationale**:
- 2 projects (small solution)
- Both currently target `net8.0` and are SDK-style
- Simple dependency structure (one library and one test project)
- All 4 NuGet packages are marked compatible with .NET 10 in the assessment

### Scope

Projects in scope:
- `DomainValidation/DomainValidation.csproj` (Class Library)
- `DomainValidation.UnitTests/DomainValidation.UnitTests.csproj` (Test project)

Target state:
- All projects target `net10.0`
- All existing NuGet packages remain at assessed-compatible versions
- Solution builds cleanly and all tests pass on .NET 10 SDK

### Key Outcomes
- Unified upgrade to `.NET 10 (LTS)` with no intermediate mixed-target state
- No expected code changes based on assessment (0+ estimated LOC to modify), but compilation on .NET 10 will validate this
- Test project runs successfully on .NET 10 using current xUnit and test SDK packages

---

## 2. Migration Strategy

### 2.1 Approach

**All-At-Once Strategy**

All projects will be upgraded to `net10.0` in a single atomic operation:
- Update target frameworks for both projects together
- Keep existing package versions, as they are marked compatible
- Restore and build the full solution once
- Fix any compilation issues discovered
- Run all tests after a successful build

This avoids maintaining multiple framework targets and keeps the upgrade simple and fast for this small solution.

### 2.2 Phases (Conceptual)

These phases describe the logical flow, while execution remains a single coordinated upgrade.

- **Phase 0 – Preparation**
  - Ensure .NET 10 SDK is installed and usable by the solution
  - Confirm branch `upgrade-to-NET10` is used for all changes

- **Phase 1 – Atomic Framework Upgrade**
  - Update `TargetFramework` for all projects from `net8.0` to `net10.0`
  - Restore and build the solution
  - Address any compilation errors/warnings caused by framework change

- **Phase 2 – Test Execution & Validation**
  - Run all tests in `DomainValidation.UnitTests`
  - Investigate and fix any test failures or runtime issues

---

## 3. Detailed Dependency Analysis

### 3.1 Project List and Roles

- `DomainValidation/DomainValidation.csproj`
  - Type: Class Library (`ClassLibrary`, SDK-style)
  - Current TF: `net8.0` ? Target TF: `net10.0`
  - Dependencies: none (no project references)
  - Dependants: `DomainValidation.UnitTests`

- `DomainValidation.UnitTests/DomainValidation.UnitTests.csproj`
  - Type: Test Project (`DotNetCoreApp`, SDK-style)
  - Current TF: `net8.0` ? Target TF: `net10.0`
  - Dependencies: `DomainValidation` project, 4 NuGet packages
  - Dependants: none

### 3.2 Dependency Graph (from assessment)

- `DomainValidation.UnitTests` ? `DomainValidation`
- No circular dependencies
- Single-level dependency depth (tests depend on library)

### 3.3 Migration Ordering (Conceptual)

Although the upgrade will be performed all at once, the dependency structure informs validation focus:
- Library `DomainValidation` must build successfully on `net10.0` to unblock tests
- `DomainValidation.UnitTests` must reference the upgraded library and run on `net10.0`

---

## 4. Project-by-Project Plans

### 4.1 `DomainValidation/DomainValidation.csproj`

#### Current State
- Target Framework: `net8.0`
- Project Type: SDK-style class library
- Dependencies: none
- Dependants: `DomainValidation.UnitTests`
- LOC: 178 across 3 files
- Assessment: 0 package or API issues; estimated LOC to modify: `0+`
- Risk Level: **Low** (small library, no external packages)

#### Target State
- Target Framework: `net10.0`
- Dependencies: unchanged
- Expected code changes: none anticipated; only recompile on .NET 10

#### Migration Steps (Specification)
1. **Framework Update**
   - In the project file `DomainValidation/DomainValidation.csproj`, update:
     - `TargetFramework` from `net8.0` to `net10.0`.

2. **Code & API Adjustments**
   - Rebuild the solution under .NET 10 as part of the atomic upgrade.
   - If any compilation errors appear (none expected based on assessment), resolve them by:
     - Updating usages of APIs that changed or became obsolete in .NET 10.
     - Adjusting nullable annotations or analyzers if stricter rules are enforced.

3. **Validation**
   - Confirm the project builds successfully as part of the full solution build on `net10.0`.
   - Ensure there are no new warnings that indicate future breaking changes (treat warnings as items to review).

#### Testing Strategy for This Project
- This library is tested indirectly via `DomainValidation.UnitTests`.
- Validation is complete when:
  - The solution build succeeds.
  - All tests in `DomainValidation.UnitTests` pass on `net10.0`.

---

### 4.2 `DomainValidation.UnitTests/DomainValidation.UnitTests.csproj`

#### Current State
- Target Framework: `net8.0`
- Project Type: SDK-style test project (`DotNetCoreApp`)
- Dependencies:
  - Project reference: `DomainValidation`
  - NuGet packages (all marked compatible in assessment):
    - `coverlet.collector` 6.0.4
    - `Microsoft.NET.Test.Sdk` 17.12.0
    - `xunit` 2.9.3
    - `xunit.runner.visualstudio` 3.0.1
- LOC: 236 across 5 files
- Assessment: 0 package issues, 0 API issues, estimated LOC to modify: `0+`
- Risk Level: **Low** (standard test stack, all packages compatible)

#### Target State
- Target Framework: `net10.0`
- Same NuGet package versions as listed above
- All tests pass when run on .NET 10

#### Migration Steps (Specification)
1. **Framework Update**
   - In `DomainValidation.UnitTests/DomainValidation.UnitTests.csproj`, update:
     - `TargetFramework` from `net8.0` to `net10.0`.

2. **Package References**
   - Keep versions as-is, since all are reported ? compatible with .NET 10 in the assessment.
   - If future compatibility issues are observed, consider upgrading within the same major range following package release notes.

3. **Code & Test Adjustments**
   - After building and running tests on .NET 10:
     - Fix any compilation errors related to test attributes, assertions, or async patterns.
     - Update test code if the test SDK or xUnit surfaces behavior changes (e.g., timing-sensitive tests, new default behaviors).

4. **Validation**
   - Run all tests in this project on .NET 10.
   - Confirm:
     - All tests pass.
     - No infrastructure issues (e.g., test host failures, collector issues).

---

## 5. Package Update Reference

From the assessment, all NuGet packages are compatible with .NET 10 and do not require version changes. For completeness:

### 5.1 Common/Test Packages

| Package | Current Version | Target Version | Projects | Status | Notes |
| :--- | :---: | :---: | :--- | :---: | :--- |
| `coverlet.collector` | 6.0.4 | 6.0.4 | `DomainValidation.UnitTests` | ? Compatible | No change required |
| `Microsoft.NET.Test.Sdk` | 17.12.0 | 17.12.0 | `DomainValidation.UnitTests` | ? Compatible | No change required |
| `xunit` | 2.9.3 | 2.9.3 | `DomainValidation.UnitTests` | ? Compatible | No change required |
| `xunit.runner.visualstudio` | 3.0.1 | 3.0.1 | `DomainValidation.UnitTests` | ? Compatible | No change required |

There are **no security vulnerabilities** or suggested upgrades flagged in the assessment for these packages.

---

## 6. Breaking Changes Catalog

The assessment reports **no API-level incompatibilities** for the analyzed code when moving from .NET 8 to .NET 10.

### 6.1 Expected Areas to Watch

Even though no specific issues were discovered, during the upgrade and subsequent builds/tests, pay attention to:

- **Runtime behavior changes**
  - Subtle changes in BCL behavior between .NET 8 and .NET 10 (e.g., performance optimizations, edge-case behavior).

- **Analyzer / compiler behavior**
  - New or stricter analyzers that might surface additional warnings; decide whether to treat these as errors or to suppress them selectively.

### 6.2 Handling Unexpected Breaking Changes

If compilation or runtime issues emerge:
- Identify the API or behavior difference.
- Check official .NET release notes and breaking-changes documentation for .NET 9 and .NET 10.
- Adjust code accordingly (e.g., use alternative overloads, update patterns, revise tests that rely on prior behavior).

---

## 7. Testing & Validation Strategy

### 7.1 Project-Level Testing

For each project:

- `DomainValidation`
  - [ ] Builds successfully targeting `net10.0`.
  - [ ] No new warnings that indicate future breaking changes (review and address where appropriate).

- `DomainValidation.UnitTests`
  - [ ] Builds successfully targeting `net10.0`.
  - [ ] All existing tests execute and pass.

### 7.2 Solution-Level Validation

After the atomic upgrade:
- [ ] Full solution builds on .NET 10 with no errors.
- [ ] All tests in `DomainValidation.UnitTests` pass.
- [ ] No package load or test host errors.

### 7.3 Regression & Behavioral Checks

Even with a small codebase, consider:
- [ ] Reviewing any domain-specific edge cases covered by tests to ensure no behavioral regressions.
- [ ] Adding tests for any uncovered critical paths if gaps are discovered.

---

## 8. Complexity & Effort Assessment

### 8.1 Per-Project Complexity

| Project | Role | LOC | Packages | Risk | Relative Complexity |
| :--- | :--- | :---: | :---: | :---: | :---: |
| `DomainValidation` | Core domain library | 178 | 0 | Low | Low |
| `DomainValidation.UnitTests` | Test project | 236 | 4 | Low | Low |

### 8.2 Overall Assessment

- Overall solution complexity: **Simple** (2 projects, shallow dependency tree, SDK-style, no incompatible packages).
- Expected upgrade difficulty: **Low**.
- Main work: updating project target frameworks and validating builds/tests.

---

## 9. Risk Management

### 9.1 Identified Risks

| Area | Description | Likelihood | Impact | Mitigation |
| :--- | :--- | :---: | :---: | :--- |
| Build/compile | Unexpected compilation errors after TF change | Low | Medium | Resolve errors immediately; consult .NET 9/10 breaking changes docs as needed |
| Test behavior | Tests relying on subtle BCL behavior may change | Low | Low–Medium | Update tests to reflect intended behavior; adjust implementation only if necessary |
| Environment | Missing or misconfigured .NET 10 SDK | Low | Medium | Verify SDK installation and any `global.json` usage before running builds |

### 9.2 Contingency / Rollback

- Because all changes are on a dedicated branch `upgrade-to-NET10`, rollback is straightforward:
  - If critical issues arise, you can revert individual commits or reset the branch, and keep `main` on .NET 8 until resolved.

---

## 10. Source Control Strategy

### 10.1 Branching

- Use `main` as the stable branch (currently on .NET 8).
- Perform all .NET 10 upgrade work on branch `upgrade-to-NET10`.

### 10.2 Commit Strategy

Use a **single, atomic commit** for the framework upgrade if feasible, reflecting the All-At-Once strategy:
- Include in the same commit:
  - Project file changes for both projects (framework updates).
  - Any minor code tweaks required to compile on .NET 10.
- Optionally, a follow-up commit may be used for test or documentation updates if changes are non-trivial.

### 10.3 Pull Request & Review

- Create a PR from `upgrade-to-NET10` into `main`.
- Recommended review checklist:
  - [ ] Both projects target `net10.0`.
  - [ ] No unintended file changes.
  - [ ] Solution builds successfully in CI using .NET 10 SDK.
  - [ ] All tests pass in CI.

---

## 11. Success Criteria

The .NET 10 upgrade is considered complete when:

### 11.1 Technical Criteria
- [ ] `DomainValidation` targets `net10.0` and builds successfully.
- [ ] `DomainValidation.UnitTests` targets `net10.0` and builds successfully.
- [ ] All NuGet packages remain compatible and restore successfully.
- [ ] Full solution build succeeds with 0 errors.

### 11.2 Testing & Quality Criteria
- [ ] All tests in `DomainValidation.UnitTests` pass on .NET 10.
- [ ] No new test failures introduced by the upgrade.
- [ ] No regressions observed in domain behavior covered by tests.

### 11.3 Process Criteria
- [ ] All changes are contained in branch `upgrade-to-NET10` and merged via PR into `main` after review.
- [ ] All-At-Once strategy followed (no partial or staged framework targets left behind).
- [ ] Documentation and CI configuration (if any) are aligned with targeting .NET 10.
