# Valr.Net Modernization Roadmap

This document captures the plan for upgrading the Valr.Net SDK. It is structured around six strategic initiatives, each broken into focused workstreams.

## 1. Dependency Uplift (CryptoExchange.Net 9.x)

**Goal:** Move from CryptoExchange.Net 5.1.8 to the current 9.x release, adopting its System.Text.Json stack and new client abstractions.

**Workstreams**
1. Assess breaking changes
   - Catalogue delta between 5.1.8 and 9.x (removed Newtonsoft.Json, new Rest/Socket base classes, option hierarchies).
   - Inventory Valr-specific usages (JToken parsing, JsonProperty attributes, request helpers) that need refactors.
2. Update options/environment abstraction
   - Create ValrRestOptions, ValrSocketOptions, and ValrEnvironment types inheriting from RestExchangeOptions<T> / SocketExchangeOptions<T>.
   - Map existing ValrClientOptions/ValrSocketClientOptions settings into the new structure.
3. Port REST clients
   - Refactor ValrClient and API sub-clients to use the new constructor pattern (Initialize, AddApiClient).
   - Replace SendRequestInternal helpers with RequestDefinition + ParameterCollection pipelines from CryptoExchange.Net 9.
   - Implement System.Text.Json serialization: custom converters, enum parsing, date formats.
4. Port WebSocket clients
   - Update ValrSocketClient and stream clients to inherit the new BaseSocketClient, wiring message accessors/serializers.
   - Rework subscribe/unsubscribe logic to emit RequestDefinition payloads and handle new heartbeat/runtime hooks.
5. Shared utilities
   - Replace usages of CryptoExchange.Net.CommonObjects/Interfaces.CommonClients with new shared abstractions or project equivalents.
   - Build internal helper extensions if 9.x removed key helpers (e.g., AddOptionalParameter).
6. Validation
   - Retarget library to net8.0 (post-refactor) to align with 9.x dependencies.
   - Run build/tests; smoke-test critical endpoints with sandbox keys.

## 2. Modernize Targets and Tooling

**Goal:** Align runtime/tooling with current .NET and DevEx best practices.

1. Target Framework & SDK
   - Move projects to net8.0 after dependency uplift.
   - Adopt latest LTS SDK in CI.
2. Build Tooling
   - Enable nullable warnings as errors, link analyzers (e.g., Microsoft.CodeAnalysis.NetAnalyzers).
   - Add editorconfig rules to enforce formatting and async patterns.
3. Packaging & CI
   - Update pipeline to use dotnet publish --os templates if needed.
   - Modernize NuGet metadata (changelog, readme, icon references) per current guidelines.

## 3. API Surface Expansion (per VALR docs)

**Goal:** Cover endpoints introduced since 2022, providing parity with VALR REST/WebSocket offerings.

1. Gap Analysis
   - Map existing coverage vs. [docs.valr.com](https://docs.valr.com/) sections (Convert, Perpetuals, Margin, Funding, Loans, Earn, Simple Swap, Batch Orders, etc.).
   - Prioritize modules based on user demand and implementation complexity.
2. Module Implementation
   - For each new API domain: add endpoints, request/response models, interfaces, and client classes.
   - Ensure request signing/auth alignment with new CryptoExchange.Net patterns.
3. Documentation & Samples
   - Update README and examples to showcase new capabilities.
   - Provide migration guidance for newly added namespaces/methods.

## 4. Stabilize Current Surface

**Goal:** Fix known defects and backfill tests to ensure regressions are detected.

1. Critical Bug Fixes
   - Resolve order placement issues (clientOrderId handling, STOP_LOSS_LIMIT guard).
   - Fix withdrawal/payments endpoints (wrong URLs, missing params).
   - Correct error parsing in ValrClient.ParseErrorResponse.
   - Repair account stream handler (Enum.TryParse logic).
2. Test Coverage
   - Add unit tests for order/trade flows, payments, withdrawals, and socket routers.
   - Introduce integration tests against sandbox keys where feasible.
3. Quality Gates
   - Configure CI to run unit/integration tests, enforce coverage thresholds.

## 5. WebSocket Overhaul

**Goal:** Align stream clients with latest VALR specs and CryptoExchange.Net socket abstractions.

1. Protocol Review
   - Compare current implementation vs. doc revisions (auth flow, message types, heartbeat expectations).
2. Client Refactor
   - Rebuild subscription infrastructure using new base connectivity patterns.
   - Introduce reconnection/backoff policies, subscription replays, and diagnostic logging.
3. Testing & Monitoring
   - Add automated smoke tests for key topics (order updates, account balances, market data).
   - Provide hooks for consumers to monitor connection health.

## 6. Release Hygiene

**Goal:** Prepare the library for a polished major release.

1. Versioning & Branch Strategy
   - Adopt semantic versioning; plan major release (e.g., 2.0.0) with breaking-change notes.
2. Documentation
   - Update README, changelog, XML docs, and samples.
   - Include upgrade guide from 1.x -> 2.x (highlighting CryptoExchange.Net 9.x migration steps).
3. Distribution & Support
   - Ensure NuGet metadata (license, icon, readme) is up to date.
   - Outline support/maintenance expectations.

---

This roadmap should be revisited after each major milestone to adjust priorities or incorporate newly discovered work.
