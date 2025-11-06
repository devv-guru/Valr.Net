# Valr.Net Upgrade Task Board

This task board breaks the roadmap into actionable work items. Tasks are grouped by theme and ordered to reflect dependencies.

## 1. Dependency Uplift (CryptoExchange.Net 9.x)

### 1.1 Investigation & Planning
- [ ] DU-1: Catalogue CryptoExchange.Net 5.1.8 vs 9.x breaking changes relevant to Valr.Net.
- [ ] DU-2: Inventory Valr.Net usages of Newtonsoft.Json, CommonObjects, and legacy request helpers.

### 1.2 Options & Environment Layer
- [ ] DU-3: Design ValrEnvironment (live/test/custom) deriving from TradeEnvironment.
- [ ] DU-4: Implement ValrRestOptions inheriting from RestExchangeOptions<ValrEnvironment>.
- [ ] DU-5: Implement ValrSocketOptions inheriting from SocketExchangeOptions<ValrEnvironment>.
- [ ] DU-6: Map existing ValrClientOptions/ValrSocketClientOptions values into the new structures.

### 1.3 REST Client Refactor
- [ ] DU-7: Update ValrClient constructor to use Initialize/AddApiClient pattern (System.Text.Json).
- [ ] DU-8: Refactor General API clients to use RequestDefinition and ParameterCollection.
- [ ] DU-9: Refactor Spot API clients accordingly; ensure trade helpers cover new shared interfaces.
- [ ] DU-10: Refactor Pay API client to new pipeline.
- [ ] DU-11: Implement System.Text.Json converters for enums/timestamps formerly covered by Newtonsoft.
- [ ] DU-12: Replace error parsing and response handling with new CryptoExchange.Net 9.x types.

### 1.4 WebSocket Client Refactor
- [ ] DU-13: Update ValrSocketClient to new base and inject IMessageSerializer/IStreamMessageAccessor.
- [ ] DU-14: Refactor Spot stream subscription helpers (request payloads, responses, reconnection).
- [ ] DU-15: Refactor General stream client, fixing routing bugs along the way.

### 1.5 Shared Utilities & Helpers
- [ ] DU-16: Recreate helper methods removed in 9.x (e.g., AddOptionalParameter).
- [ ] DU-17: Update authentication provider to align with new credential handling.
- [ ] DU-18: Ensure rate-limiter configuration matches 9.x expectations.

### 1.6 Validation
- [ ] DU-19: Retarget projects to net8.0 and update test dependencies once code compiles.
- [ ] DU-20: Run build/tests; smoke-test core endpoints against sandbox API keys.
- [ ] DU-21: Document migration notes for downstream consumers.

## 2. Modernize Targets & Tooling

- [ ] MT-1: Update global.json/CI to use .NET 8 LTS SDK.
- [ ] MT-2: Enable nullable warnings as errors; clean existing warnings where feasible.
- [ ] MT-3: Add Microsoft.CodeAnalysis.NetAnalyzers and configure editorconfig rules.
- [ ] MT-4: Review packaging metadata (license, icon, changelog, readme in NuGet package).
- [ ] MT-5: Update build scripts / CI workflows.

## 3. API Surface Expansion

- [ ] AE-1: Perform coverage gap analysis vs latest VALR docs.
- [ ] AE-2: Prioritize new modules (Convert, Perpetuals, Margin, Funding, Loans, Earn, Simple Swap, Batch Orders).
- [ ] AE-3: Implement Convert API (REST + models + tests).
- [ ] AE-4: Implement Perpetuals API coverage.
- [ ] AE-5: Implement Margin API coverage.
- [ ] AE-6: Implement Funding/Loans/Earn modules.
- [ ] AE-7: Implement Batch Orders / Simple Swap flows.
- [ ] AE-8: Update README/examples for new endpoints.

## 4. Stabilize Current Surface

- [ ] ST-1: Fix clientOrderId bug in ValrClientSpotApiTrading.
- [ ] ST-2: Fix STOP_LOSS_LIMIT guard logic.
- [ ] ST-3: Correct order status/detail endpoints (customerOrderId path).
- [ ] ST-4: Correct crypto withdrawal endpoint usage.
- [ ] ST-5: Fix Pay API senderNote/status filtering.
- [ ] ST-6: Fix account stream routing Enum.TryParse bug.
- [ ] ST-7: Improve error handling in ValrClient.ParseErrorResponse.
- [ ] ST-8: Add unit tests covering bugs ST-1..ST-7.
- [ ] ST-9: Add integration tests for critical flows (orders, payments, withdrawals).

## 5. WebSocket Overhaul

- [ ] WS-1: Review updated VALR WebSocket specs (public + authenticated).
- [ ] WS-2: Define reconnection/backoff strategy and heartbeat handling.
- [ ] WS-3: Implement subscription lifecycle improvements in spot streams.
- [ ] WS-4: Implement account stream upgrades and event routing tests.
- [ ] WS-5: Create automated smoke tests for core topics.
- [ ] WS-6: Document monitoring hooks/diagnostics for consumers.

## 6. Release Hygiene

- [ ] RH-1: Define versioning strategy and release checklist.
- [ ] RH-2: Draft migration guide (1.x -> 2.x).
- [ ] RH-3: Update README/changelog with new features and breaking changes.
- [ ] RH-4: Verify NuGet packaging (readme, icon, license) and publishing flow.
- [ ] RH-5: Plan communication/support policy for the new release.

---

**Usage Notes**
- Prefixes (DU, MT, AE, ST, WS, RH) map tasks to roadmap sections.
- Tasks can be assigned to contributors in issue tracker/board software.
- Re-evaluate priorities after the dependency uplift milestone, adjusting downstream work accordingly.
