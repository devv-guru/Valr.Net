# Dependency Uplift (CryptoExchange.Net 5.1.8 -> 9.x) - Findings and Plan

This document summarizes the breaking changes that impact `Valr.Net` when upgrading from `CryptoExchange.Net` 5.1.8 to 9.x, the current touchpoints in the codebase, proposed option/environment stubs, and open questions.

## 1) High level breaking changes (DU-1)

- Newtonsoft.Json removed in favor of `System.Text.Json`.
  - Impact: Any code using `JToken`, `JsonProperty` attributes, custom `JsonConverter` implementations for Newtonsoft must be migrated.
- New base client abstractions and options
  - CryptoExchange.Net 9.x introduces new base types and option hierarchies (e.g. `RestExchangeOptions<T>`, `SocketExchangeOptions<T>`, new `ApiClient`/`ClientOptions` patterns).
  - `SendRequestInternal` helpers and older request pipeline approaches are replaced by `RequestDefinition` + `ParameterCollection` pipelines.
- Socket/REST base classes changed
  - REST and WebSocket clients are expected to inherit and be initialized differently (initialize + add sub-client pattern).
- CommonObjects / CommonClients changes
  - Types that used to live under `CryptoExchange.Net.CommonObjects` and `CryptoExchange.Net.Interfaces.CommonClients` were changed or removed. `CommonOrderSide`, `CommonOrderStatus`, `CommonOrderType` and helper conversion types can be affected.
- Rate limiter and options model changes
  - Rate limiter construction, location and configuration types have changed in 9.x (options moved under new ApiClientOptions-like types).
- Serializer / enum parsing changes
  - System.Text.Json requires different custom converters. Enum handling and date/time formats must be ported.

## 2) Valr.Net touchpoints (DU-2)

These files refer to types or patterns that will need attention:

- `Valr.Net/Objects/Options/ValrClientOptions.cs`
  - Uses `BaseRestClientOptions`, `ValrApiClientOptions`, `IRateLimiter`, `RateLimiter` and config patterns from 5.x.
  - Maps three `ValrApiClientOptions` instances (Spot, Pay, General) using `ValrApiAddresses.Default.RestClientAddress`.

- `Valr.Net/Objects/Options/ValrSocketClientOptions.cs`
  - Inherits `BaseSocketClientOptions` and uses `ApiClientOptions`.
  - Contains `SpotStreamsOptions` and `GeneralStreamsOptions`.

- `Valr.Net/Objects/Options/VarlApiClientOptions.cs` (note: file name contains a small typo `Varl`)
  - Inherits `RestApiClientOptions`. Contains `TimestampOffset`, `TradeRulesBehaviour`, `TradeRulesUpdateInterval`.

- `Valr.Net/Objects/Options/ValrOrderBookOptions.cs`
  - Uses `OrderBookOptions` and references `IValrClient` / `IValrSocketClient`.

- `Valr.Net/Converters/EnumConverter.cs`
  - Uses `CryptoExchange.Net.CommonObjects` types: `CommonOrderSide`, `CommonOrderStatus`, `CommonOrderType` and performs conversions.

- Indirect usages to search and verify (not exhaustive):
  - Any references to `Newtonsoft.Json`, `JToken`, `JsonConvert`, `JsonProperty` attributes.
  - Any custom converters or serialization helpers.
  - Calls to legacy helper methods like `SendRequestInternal`, request helpers and internal rate limiter builders.

## 3) Types of refactors required

- Replace Newtonsoft usages with System.Text.Json equivalents
  - Implement `JsonSerializer` conversions and `JsonConverter<T>` where necessary.
  - Replace `JToken` parsing with `JsonDocument` / `JsonElement` or strongly typed deserialization.

- Replace legacy base option and client types with 9.x equivalents
  - Transform `ValrClientOptions`/`ValrSocketClientOptions` to map into the new `RestExchangeOptions<T>`/`SocketExchangeOptions<T>` strategy used by 9.x.
  - Create `ValrRestOptions`, `ValrSocketOptions` and `ValrEnvironment` to act as the new configuration surface and to map existing settings.

- Port request/response pipeline
  - Replace `SendRequestInternal` usage with the `RequestDefinition` + `ParameterCollection` pipeline from CryptoExchange.Net 9.x. This will require reworking REST sub-clients.

- Adapt WebSocket clients
  - Update `ValrSocketClient` and stream clients to inherit new socket base classes. Rework subscribe/unsubscribe flows.

- Replace `CryptoExchange.Net.CommonObjects` usage
  - Provide internal equivalents or mapping adapters for common types (e.g., order enums) if the new library removes/relocates them.

## 4) Proposed stubs (DU-3 – DU-6)

I added small stub files under `Valr.Net/Objects/Options/` to scaffold the migration. These are intentionally implementation-light and contain `TODO` notes explaining the intended inheritance and mapping once `CryptoExchange.Net` 9.x is adopted.

- `ValrRestOptions.cs` (stub)
  - Purpose: eventual inheritor of `RestExchangeOptions<T>` (CryptoExchange.Net 9.x). Holds defaults that map from `ValrClientOptions`.
  - TODOs: inherit correct base type, add mapping ctor from `ValrClientOptions`.

- `ValrSocketOptions.cs` (stub)
  - Purpose: eventual inheritor of `SocketExchangeOptions<T>` (CryptoExchange.Net 9.x). Holds socket-specific client options.
  - TODOs: inherit correct base type, add mapping ctor from `ValrSocketClientOptions`.

- `ValrEnvironment.cs` (stub)
  - Purpose: encapsulate addresses/endpoints for different environments (Live, Test, Sandbox). Replace or complement existing `ValrApiAddresses`.
  - TODOs: align with `CryptoExchange.Net` 9.x environment/address patterns and provide mapping to `ApiClientOptions`.

Files created:
- `Valr.Net/Objects/Options/ValrRestOptions.cs` (stub)
- `Valr.Net/Objects/Options/ValrSocketOptions.cs` (stub)
- `Valr.Net/Objects/Options/ValrEnvironment.cs` (stub)

## 5) Open questions / external dependencies

- Are we locking to a specific `CryptoExchange.Net` 9.x version? (9.12.0 is available in the environment but choose a stable release to target.)
- Do we want to keep `ValrApiAddresses` or replace it with `ValrEnvironment`? There is some duplication risk.
- How do we want to expose `CommonObjects` equivalents? Options:
  - Keep using CryptoExchange.Net's CommonObjects if present in 9.x and import them.
  - Introduce internal `ValrCommon` types and mapping layers (safer for future-proofing).
- Integration tests: we will need sandbox keys and smoke tests to validate the ported clients. Some behaviour changes (timing, rate limits) may break tests.

## 6) Next steps / recommended plan

1. Add `CryptoExchange.Net` 9.x to a branch and run compilation to see the exact API deltas.
2. Replace Newtonsoft dependencies first and port converters to `System.Text.Json`.
3. Create `ValrRestOptions` and `ValrSocketOptions` that inherit from 9.x base option classes, map existing `ValrClientOptions` -> new options.
4. Port REST clients to `RequestDefinition` pipeline incrementally: start with low-risk endpoints and tests.
5. Port socket clients to the new base classes and rework subscription lifecycle.
6. Run a full integration test pass against sandbox.


---

# Locations referenced (quick index)

- `Valr.Net/Objects/Options/ValrClientOptions.cs`
- `Valr.Net/Objects/Options/ValrSocketClientOptions.cs`
- `Valr.Net/Objects/Options/VarlApiClientOptions.cs` (note the `Varl` filename)
- `Valr.Net/Objects/Options/ValrOrderBookOptions.cs`
- `Valr.Net/Converters/EnumConverter.cs`


End of report.
