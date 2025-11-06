# VALR API Documentation

## Overview

VALR is a South African cryptocurrency exchange platform. This document provides comprehensive API documentation for the VALR REST API and WebSocket streams.

**Base URLs:**
- REST API: `https://api.valr.com`
- WebSocket (Trading): `wss://api.valr.com/ws/trade`
- WebSocket (Account): `wss://api.valr.com/ws/account`

**Rate Limits:**
- 500 requests per minute (per API key)
- Default receive window: 5 seconds

**Official Documentation:** https://docs.valr.com/

---

## Table of Contents

1. [Authentication](#authentication)
2. [General API](#general-api)
   - [Exchange Data](#exchange-data)
   - [Account](#account)
   - [Sub-Accounts](#sub-accounts)
   - [Crypto Wallet](#crypto-wallet)
   - [Fiat Wallet](#fiat-wallet)
   - [Wire Wallet](#wire-wallet)
3. [Spot API](#spot-api)
   - [Trading](#trading)
   - [Instant Trading](#instant-trading)
4. [Pay API](#pay-api)
5. [WebSocket Streams](#websocket-streams)
   - [Spot Streams](#spot-streams)
   - [Account Streams](#account-streams)
6. [Data Types & Enums](#data-types--enums)

---

## Authentication

VALR API uses API key authentication. You need to:
1. Generate an API key from your VALR account
2. Include the API key in request headers
3. Sign requests with your API secret

**Required Headers:**
- `X-VALR-API-KEY`: Your API key
- `X-VALR-SIGNATURE`: HMAC-SHA512 signature
- `X-VALR-TIMESTAMP`: Unix timestamp in milliseconds

---

## General API

### Exchange Data

Public and authenticated endpoints for market data.

#### Get System Status
**GET** `/v1/public/status`

Get the current system status of the VALR exchange.

**Authentication:** Not required

**Documentation:** https://docs.valr.com/#16ccc087-4f8c-49b0-aa43-fd4f472c6a52

---

#### Get Server Time
**GET** `/v1/public/time`

Get the current server time in Unix timestamp format (milliseconds).

**Authentication:** Not required

**Documentation:** https://docs.valr.com/#95f84056-2ac7-4f92-a5d9-fd0d9c104f01

---

#### Get Supported Currencies
**GET** `/v1/public/currencies`

Get all currencies supported by VALR.

**Authentication:** Not required

**Response:** Array of currency objects with details including:
- Currency code
- Decimal places
- Long name
- Short name
- Supported operations

**Documentation:** https://docs.valr.com/#88ab52a2-d63b-48b2-8984-d0982baec40a

---

#### Get Supported Currency Pairs
**GET** `/v1/public/pairs`

Get all currency pairs supported by VALR.

**Authentication:** Not required

**Response:** Array of currency pair objects including:
- Currency pair symbol
- Base currency
- Quote currency
- Short name
- Active status

**Documentation:** https://docs.valr.com/#cfa57d7e-2106-4066-bc27-c10210b6aa82

---

#### Get Order Types for Currency Pair
**GET** `/v1/public/ordertypes/:currencyPair`

Get all order types supported for a specific currency pair.

**Authentication:** Not required

**Path Parameters:**
- `currencyPair` (string, required): Currency pair symbol (e.g., "BTCZAR")

**Response:** Array of supported order types for the pair

**Documentation:** https://docs.valr.com/#700eddaa-60ba-4872-ae2b-577c3285d695

---

#### Get Market Summary (All Pairs)
**GET** `/v1/public/marketsummary`

Get market summary for all currency pairs.

**Authentication:** Not required

**Response:** Array of market summary objects including:
- Currency pair
- Ask/Bid prices
- Last traded price
- Previous close price
- Base/Quote volume
- High/Low price
- Price change percentage

**Documentation:** https://docs.valr.com/#cd1f0448-3da3-44cf-b00d-91edd74e7e19

---

#### Get Market Summary for Specific Pair
**GET** `/v1/public/marketsummary/:currencyPair`

Get market summary for a specific currency pair.

**Authentication:** Not required

**Path Parameters:**
- `currencyPair` (string, required): Currency pair symbol

**Documentation:** https://docs.valr.com/#89b446bb-60a6-42ff-aa09-29e4918a9eb0

---

#### Get Trade History
**GET** `/v1/public/:currencyPair/trades`

Get the last 100 recent trades for a currency pair.

**Authentication:** Not required

**Path Parameters:**
- `currencyPair` (string, required): Currency pair symbol

**Query Parameters:**
- `skip` (integer, optional): Number of items to skip (0-100)
- `limit` (integer, optional): Number of items to return (1-100)
- `before` (long, optional): Get trades before this ID

**Response:** Array of trade objects including:
- Price
- Quantity
- Currency pair
- Traded at timestamp
- Taker side (buy/sell)
- Sequential ID
- Quote volume

**Documentation:**
- Base: https://docs.valr.com/#68ecbf66-c8ab-4460-a1f3-5b245b15877e
- Filtered: https://docs.valr.com/#f34f0b86-3f74-456f-b72c-93baa57ad20c

---

#### Get Public Order Book (Aggregated)
**GET** `/v1/public/:currencyPair/orderbook`

**Alternative:** `/v1/marketdata/:currencyPair/orderbook`

Get aggregated order book for a currency pair (public, no authentication required).

**Authentication:** Not required

**Path Parameters:**
- `currencyPair` (string, required): Currency pair symbol

**Response:** Order book with:
- Asks: Array of [price, quantity] tuples
- Bids: Array of [price, quantity] tuples
- Last change timestamp
- Sequence number

**Documentation:** https://docs.valr.com/#720fec1b-a1f6-486a-b04a-7ae76c6f9f66

---

#### Get Public Order Book (Full)
**GET** `/v1/public/:currencyPair/orderbook/full`

**Alternative:** `/v1/marketdata/:currencyPair/orderbook/full`

Get full order book with all individual orders (public).

**Authentication:** Not required

**Path Parameters:**
- `currencyPair` (string, required): Currency pair symbol

**Response:** Full order book including:
- Individual order IDs
- Complete price levels
- Order quantities
- Sequence number

**Documentation:** https://docs.valr.com/#9ee254bd-4361-40e8-95a1-f57e74968f24

---

#### Get Authenticated Order Book (Aggregated)
**GET** `/v1/marketdata/:currencyPair/orderbook`

Get aggregated order book with authenticated access (may have additional data).

**Authentication:** Required

**Path Parameters:**
- `currencyPair` (string, required): Currency pair symbol

**Documentation:** https://docs.valr.com/#926f9245-35d1-4bca-a114-0af07bc229f7

---

#### Get Authenticated Order Book (Full)
**GET** `/v1/marketdata/:currencyPair/orderbook/full`

Get full order book with authenticated access.

**Authentication:** Required

**Path Parameters:**
- `currencyPair` (string, required): Currency pair symbol

**Documentation:** https://docs.valr.com/#c2acf6b9-dbba-4e6a-9075-a7907360812d

---

### Account

Authenticated endpoints for account management.

#### Get Account Balances
**GET** `/v1/account/balances`

Get all account balances for your VALR account.

**Authentication:** Required

**Response:** Array of balance objects including:
- Currency
- Available balance
- Reserved balance (in orders)
- Total balance
- Updated at timestamp

**Documentation:** https://docs.valr.com/#60455ec7-ecdc-42ad-9a57-64941299da52

---

#### Get Transaction History
**GET** `/v1/account/transactionhistory`

Get transaction history for your account.

**Authentication:** Required

**Query Parameters:**
- `skip` (integer, optional): Number of items to skip (0-100)
- `limit` (integer, optional): Number of items to return (1-200, default: 100)
- `transactionTypes` (array, optional): Filter by transaction types
- `currency` (string, optional): Filter by currency
- `startTime` (datetime, optional): Start time filter
- `endTime` (datetime, optional): End time filter
- `beforeId` (string, optional): Get transactions before this ID

**Transaction Types:**
- BLOCKCHAIN_RECEIVE
- BLOCKCHAIN_SEND
- FIAT_DEPOSIT
- FIAT_WITHDRAWAL
- REFERRAL_REBATE
- REFERRAL_REWARD
- PROMOTION_REWARD
- INTERNAL_TRANSFER
- And more...

**Response:** Array of transaction objects

**Documentation:**
- Base: https://docs.valr.com/#a84bf578-adb8-4023-8aa0-5f74550490a8
- Filtered: https://docs.valr.com/#0d7cc0ff-b8ca-4e1f-980e-36d07672e53d
- Before ID: https://docs.valr.com/#c2f6db65-1fe4-4561-9e5e-d28d6a4fca9d

---

#### Get Trade History for Currency Pair
**GET** `/v1/account/:currencyPair/tradehistory`

Get your personal trade history for a specific currency pair.

**Authentication:** Required

**Path Parameters:**
- `currencyPair` (string, required): Currency pair symbol

**Query Parameters:**
- `limit` (integer, optional): Number of items to return (1-100, default: 10)
- `skip` (integer, optional): Number of items to skip
- `startTime` (datetime, optional): Start time filter
- `endTime` (datetime, optional): End time filter
- `beforeId` (string, optional): Get trades before this ID

**Response:** Array of trade objects including:
- Price
- Quantity
- Currency pair
- Traded at timestamp
- Side (buy/sell)
- Order ID

**Documentation:** https://docs.valr.com/#11856958-9461-490e-9e01-4b1f5a2097ae

---

### Sub-Accounts

Manage sub-accounts and transfer funds between accounts.

#### Get Sub-Accounts
**GET** `/v1/account/subaccounts`

Get all sub-accounts associated with your master account.

**Authentication:** Required

**Response:** Array of sub-account objects including:
- Sub-account ID
- Label/Name
- Status

**Documentation:** https://docs.valr.com/#9443d7ce-c1c5-4597-b43e-d8fc2e7b49a7

---

#### Get All Account Balances (Master + Sub-Accounts)
**GET** `/v1/account/balances/all`

Get balances for master account and all sub-accounts.

**Authentication:** Required

**Response:** Array of account balance objects including:
- Account ID
- Account type (master/sub)
- Currency balances

**Documentation:** https://docs.valr.com/#f690f824-c254-4d85-8013-27e317addf67

---

#### Create Sub-Account
**POST** `/v1/account/subaccount`

Create a new sub-account.

**Authentication:** Required

**Request Body:**
```json
{
  "label": "string"
}
```

**Parameters:**
- `label` (string, required): Name/label for the sub-account (max 50 characters)

**Response:** Created sub-account details

**Documentation:** https://docs.valr.com/#ee3e19d6-a530-441d-aaf6-a526d368ff82

---

#### Transfer Between Accounts
**POST** `/v1/account/subaccounts/transfer`

Transfer funds between master account and sub-accounts.

**Authentication:** Required

**Request Body:**
```json
{
  "fromId": "string",
  "toId": "string",
  "currencyCode": "string",
  "amount": "decimal"
}
```

**Parameters:**
- `fromId` (string, required): Source account ID
- `toId` (string, required): Destination account ID
- `currencyCode` (string, required): Currency to transfer
- `amount` (decimal, required): Amount to transfer

**Documentation:** https://docs.valr.com/#f065f4d0-bde5-4793-874d-3b2c67f5e42d

---

### Crypto Wallet

Manage cryptocurrency deposits and withdrawals.

#### Get Deposit Address
**GET** `/v1/wallet/crypto/:currencyCode/deposit/address`

Get deposit address for a cryptocurrency.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Currency code (e.g., "BTC", "ETH")

**Response:** Deposit address details including:
- Address
- Network/chain information
- Additional information (memo, tag, etc. if applicable)

**Documentation:** https://docs.valr.com/#1b89c5d6-e2f4-44e8-98f6-00d3bb5ee1a9

---

#### Get Withdrawal Information
**GET** `/v1/wallet/crypto/:currencyCode/withdraw`

Get withdrawal information and limits for a cryptocurrency.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Currency code

**Response:** Withdrawal information including:
- Minimum withdrawal amount
- Maximum withdrawal amount
- Withdrawal fees

**Documentation:** https://docs.valr.com/#1029979e-2e2e-4870-9b8c-c69eaa890757

---

#### Create Crypto Withdrawal
**POST** `/v1/wallet/crypto/:currencyCode/withdraw`

Withdraw cryptocurrency to an external address.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Currency code

**Request Body:**
```json
{
  "amount": "decimal",
  "address": "string",
  "paymentReference": "string"
}
```

**Parameters:**
- `amount` (decimal, required): Amount to withdraw
- `address` (string, required): Destination address
- `paymentReference` (string, optional): Reference note (max 50 characters)

**Response:** Withdrawal details including ID and status

**Documentation:** https://docs.valr.com/#b2e0cd0e-d0b8-466c-b225-8f92b7b1f04f

---

#### Get Withdrawal Status
**GET** `/v1/wallet/crypto/:currencyCode/withdraw/:withdrawId`

Get the status of a specific withdrawal.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Currency code
- `withdrawId` (string, required): Withdrawal ID

**Response:** Withdrawal status details

**Documentation:** https://docs.valr.com/#62da7a9d-4f0c-4a44-8e01-f40bc5f9b4c2

---

#### Get Withdrawal History
**GET** `/v1/wallet/crypto/:currencyCode/withdraw/history`

Get withdrawal history for a cryptocurrency.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Currency code

**Query Parameters:**
- `skip` (integer, optional): Number to skip (0-100)
- `limit` (integer, optional): Number to return (1-200)

**Response:** Array of withdrawal records

**Documentation:** https://docs.valr.com/#6b82b2e7-7ef6-461e-9a87-c1c1f2f9668c

---

#### Get Deposit History
**GET** `/v1/wallet/crypto/:currencyCode/deposit/history`

Get deposit history for a cryptocurrency.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Currency code

**Query Parameters:**
- `skip` (integer, optional): Number to skip (0-100)
- `limit` (integer, optional): Number to return (1-200)

**Response:** Array of deposit records

**Documentation:** https://docs.valr.com/#ab3ede5b-5bc9-4d0c-895d-32e3f66d62b8

---

#### Get Whitelisted Withdrawal Addresses
**GET** `/v1/wallet/crypto/:currencyCode/whitelist`

Get all whitelisted withdrawal addresses for a cryptocurrency.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Currency code

**Response:** Array of whitelisted addresses

---

#### Add Whitelisted Withdrawal Address
**POST** `/v1/wallet/crypto/:currencyCode/whitelist`

Add a new address to the withdrawal whitelist.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Currency code

**Request Body:**
```json
{
  "address": "string",
  "label": "string"
}
```

---

### Fiat Wallet

Manage fiat currency (ZAR) deposits and withdrawals.

#### Get Bank Accounts
**GET** `/v1/wallet/fiat/:currencyCode/accounts`

Get linked bank accounts for fiat currency.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Fiat currency code (e.g., "ZAR")

**Response:** Array of bank account objects

**Documentation:** https://docs.valr.com/#e7b13f1d-9740-4452-9653-141e1055d03b

---

#### Get Deposit Reference
**GET** `/v1/wallet/fiat/:currencyCode/deposit/reference`

Get your unique deposit reference for fiat deposits.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Fiat currency code

**Response:** Deposit reference details including:
- Unique reference code
- Bank details
- Instructions

**Documentation:** https://docs.valr.com/#619d83fa-f562-4ed3-a573-81afbafd2f1c

---

#### Create Fiat Withdrawal
**POST** `/v1/wallet/fiat/:currencyCode/withdraw`

Withdraw fiat currency to a linked bank account.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Fiat currency code

**Request Body:**
```json
{
  "linkedBankAccountId": "string",
  "amount": "decimal"
}
```

**Parameters:**
- `linkedBankAccountId` (string, required): ID of the linked bank account
- `amount` (decimal, required): Amount to withdraw

**Response:** Withdrawal details

**Documentation:** https://docs.valr.com/#fb4db187-530b-4632-b933-7bdfd192bcf5

---

### Wire Wallet

Manage international wire transfers.

#### Get Wire Accounts
**GET** `/v1/wallet/wire/:currencyCode/accounts`

Get linked wire transfer accounts.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Currency code

**Response:** Array of wire account objects

---

#### Get Wire Deposit Instructions
**GET** `/v1/wallet/wire/:currencyCode/deposit/instructions`

Get instructions for wire deposits.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Currency code

**Response:** Wire deposit instructions including:
- Bank details
- SWIFT codes
- Reference information

---

#### Create Wire Withdrawal
**POST** `/v1/wallet/wire/:currencyCode/withdraw`

Create a wire transfer withdrawal.

**Authentication:** Required

**Path Parameters:**
- `currencyCode` (string, required): Currency code

**Request Body:**
```json
{
  "linkedBankAccountId": "string",
  "amount": "decimal"
}
```

---

## Spot API

### Trading

Order management and trading endpoints.

#### Place Limit Order
**POST** `/v1/orders/limit`

Place a limit order on the exchange.

**Authentication:** Required

**Request Body:**
```json
{
  "side": "BUY|SELL",
  "quantity": "decimal",
  "price": "decimal",
  "pair": "string",
  "postOnly": "boolean",
  "customerOrderId": "string",
  "timeInForce": "GTC|FOK|IOC"
}
```

**Parameters:**
- `side` (string, required): Order side - "BUY" or "SELL"
- `quantity` (decimal, required): Order quantity in base currency
- `price` (decimal, required): Limit price in quote currency
- `pair` (string, required): Currency pair (e.g., "BTCZAR")
- `postOnly` (boolean, optional): Post-only flag (default: true)
- `customerOrderId` (string, optional): Your custom order ID (max 50 chars)
- `timeInForce` (string, optional): Time in force - GTC (default), FOK, or IOC

**Time In Force:**
- `GTC` (Good Till Cancelled): Order remains until filled or cancelled
- `FOK` (Fill Or Kill): Order must be filled immediately and completely or cancelled
- `IOC` (Immediate Or Cancel): Fill whatever possible immediately, cancel the rest

**Response:** Order details including:
- Order ID
- Status
- Remaining quantity
- Filled quantity

**Documentation:** https://docs.valr.com/#5beb7328-24ca-4d8a-84f2-6029725ad923

---

#### Place Market Order
**POST** `/v1/orders/market`

Place a market order (only available for crypto-to-ZAR pairs).

**Authentication:** Required

**Request Body:**
```json
{
  "side": "BUY|SELL",
  "baseAmount": "decimal",
  "quoteAmount": "decimal",
  "pair": "string",
  "customerOrderId": "string"
}
```

**Parameters:**
- `side` (string, required): Order side - "BUY" or "SELL"
- `baseAmount` (decimal, optional): Amount in base currency (for SELL orders)
- `quoteAmount` (decimal, optional): Amount in quote currency (for BUY orders)
- `pair` (string, required): Currency pair
- `customerOrderId` (string, optional): Your custom order ID

**Note:** For BUY orders, specify `quoteAmount`. For SELL orders, specify `baseAmount`.

**Response:** Order details

**Documentation:** https://docs.valr.com/#e1892b20-2b2a-44cf-a67b-1d86def85ec4

---

#### Place Stop-Loss/Take-Profit Limit Order
**POST** `/v1/orders/stop-limit`

Place a stop-loss or take-profit limit order.

**Authentication:** Required

**Request Body:**
```json
{
  "side": "BUY|SELL",
  "quantity": "decimal",
  "price": "decimal",
  "stopPrice": "decimal",
  "pair": "string",
  "orderType": "STOP_LOSS_LIMIT|TAKE_PROFIT_LIMIT",
  "customerOrderId": "string",
  "timeInForce": "GTC|FOK|IOC"
}
```

**Parameters:**
- `side` (string, required): Order side
- `quantity` (decimal, required): Order quantity
- `price` (decimal, required): Limit price
- `stopPrice` (decimal, required): Stop trigger price
- `pair` (string, required): Currency pair
- `orderType` (string, required): "STOP_LOSS_LIMIT" or "TAKE_PROFIT_LIMIT"
- `customerOrderId` (string, optional): Custom order ID
- `timeInForce` (string, optional): Time in force

**Stop-Loss:** Triggered when market price drops to or below stop price
**Take-Profit:** Triggered when market price rises to or above stop price

**Documentation:** https://docs.valr.com/#4bdd004a-a7a0-4d75-b018-d0e4b7316614

---

#### Get Order Status by ID
**GET** `/v1/orders/:currencyPair/orderid/:orderId`

Get the status of an order by order ID.

**Authentication:** Required

**Path Parameters:**
- `currencyPair` (string, required): Currency pair
- `orderId` (string, required): Order ID

**Response:** Order status details

**Documentation:** https://docs.valr.com/#4bdd004a-a7a0-4d75-b018-d0e4b7316614

---

#### Get Order Status by Customer Order ID
**GET** `/v1/orders/:currencyPair/customerorderid/:customerOrderId`

Get the status of an order by your custom order ID.

**Authentication:** Required

**Path Parameters:**
- `currencyPair` (string, required): Currency pair
- `customerOrderId` (string, required): Your custom order ID

**Response:** Order status details

**Documentation:** https://docs.valr.com/#87c78a99-c94c-4b16-a986-5957a62a66fc

---

#### Get All Open Orders
**GET** `/v1/orders/open`

Get all open orders across all currency pairs.

**Authentication:** Required

**Response:** Array of open order objects

**Documentation:** https://docs.valr.com/#910bc498-b88d-48e8-b392-6cc94b8cb66d

---

#### Get Order History
**GET** `/v1/orders/history`

Get historical orders with filtering options.

**Authentication:** Required

**Query Parameters:**
- `skip` (integer, optional): Number to skip (0-1000)
- `limit` (integer, optional): Number to return (1-500, default: 100)
- `orderId` (string, optional): Filter by specific order ID

**Response:** Array of historical order objects with summary information

**Documentation:** https://docs.valr.com/#5f0ef16a-4f9d-40f3-bcdf-b1a63a0b42a4

---

#### Get Order History Summary by ID
**GET** `/v1/orders/history/summary/orderid/:orderId`

Get summary information for a historical order by order ID.

**Authentication:** Required

**Path Parameters:**
- `orderId` (string, required): Order ID

**Response:** Order summary including:
- Order ID
- Order type
- Side
- Quantity
- Filled quantity
- Average price
- Status
- Timestamps

**Documentation:** https://docs.valr.com/#7f42e4d5-c853-4da2-9c7d-adb4f3385ca2

---

#### Get Order History Summary by Customer Order ID
**GET** `/v1/orders/history/summary/customerorderid/:customerOrderId`

Get summary information for a historical order by custom order ID.

**Authentication:** Required

**Path Parameters:**
- `customerOrderId` (string, required): Your custom order ID

**Response:** Order summary

**Documentation:** https://docs.valr.com/#112c551e-4ee3-46a3-8fcf-0db07d3f48f2

---

#### Get Order History Details by ID
**GET** `/v1/orders/history/detail/orderid/:orderId`

Get detailed information including all fills for a historical order.

**Authentication:** Required

**Path Parameters:**
- `orderId` (string, required): Order ID

**Response:** Detailed order information including:
- Complete order details
- All trade fills
- Fees per fill
- Timestamps

**Documentation:** https://docs.valr.com/#a5d5dbcd-e034-422c-acec-4257e3c12e2d

---

#### Get Order History Details by Customer Order ID
**GET** `/v1/orders/history/detail/customerorderid/:customerOrderId`

Get detailed information for a historical order by custom order ID.

**Authentication:** Required

**Path Parameters:**
- `customerOrderId` (string, required): Your custom order ID

**Response:** Detailed order information with fills

**Documentation:** https://docs.valr.com/#ed7fbcb1-550f-4b73-8b48-273f5ee78cdb

---

#### Cancel Order by ID
**DELETE** `/v1/orders/order`

Cancel an order by order ID or customer order ID.

**Authentication:** Required

**Request Body:**
```json
{
  "orderId": "string",
  "customerOrderId": "string",
  "pair": "string"
}
```

**Parameters:**
- `orderId` (string, optional): Order ID (provide either this or customerOrderId)
- `customerOrderId` (string, optional): Customer order ID
- `pair` (string, required): Currency pair

**Response:** Cancellation confirmation

**Documentation:** https://docs.valr.com/#3d9ba169-7222-4c0f-ab08-87c22162c0c4

---

### Instant Trading

Simple instant buy/sell endpoints for quick trading.

#### Get Quote
**POST** `/v1/simple/:currencyPair/quote`

Get a quote for an instant buy or sell order.

**Authentication:** Required

**Path Parameters:**
- `currencyPair` (string, required): Currency pair

**Request Body:**
```json
{
  "payInCurrency": "string",
  "payAmount": "decimal",
  "side": "BUY|SELL"
}
```

**Parameters:**
- `payInCurrency` (string, required): Currency you're paying with
- `payAmount` (decimal, required): Amount you're paying
- `side` (string, required): "BUY" or "SELL"

**Response:** Quote details including:
- Quote ID
- Currency pair
- Pay amount and currency
- Receive amount and currency
- Price
- Quote expiry time

**Documentation:** https://docs.valr.com/#8c1df98d-5878-44f0-9090-2211f793fd6f

---

#### Place Instant Order
**POST** `/v1/simple/:currencyPair/order`

Execute an instant order using a quote.

**Authentication:** Required

**Path Parameters:**
- `currencyPair` (string, required): Currency pair

**Request Body:**
```json
{
  "quoteId": "string",
  "payInCurrency": "string",
  "payAmount": "decimal",
  "side": "BUY|SELL"
}
```

**Parameters:**
- `quoteId` (string, required): Quote ID from Get Quote endpoint
- `payInCurrency` (string, required): Currency you're paying with
- `payAmount` (decimal, required): Amount you're paying
- `side` (string, required): "BUY" or "SELL"

**Response:** Order execution details including:
- Order ID
- Success status
- Amounts
- Price

**Documentation:** https://docs.valr.com/#b064c7f3-d789-47ea-a427-e86a8039fdda

---

#### Get Instant Order Status
**GET** `/v1/simple/:currencyPair/order/:orderId`

Get the status of an instant order.

**Authentication:** Required

**Path Parameters:**
- `currencyPair` (string, required): Currency pair
- `orderId` (string, required): Order ID

**Response:** Order status details

**Documentation:** https://docs.valr.com/#c001557e-0356-4b5f-92cf-64d3b2fe98ed

---

## Pay API

VALR Pay enables peer-to-peer cryptocurrency payments using phone numbers, email addresses, or payment IDs.

### Create Payment
**POST** `/v1/pay`

Send cryptocurrency to another VALR user.

**Authentication:** Required

**Request Body:**
```json
{
  "identifier": "string",
  "identifierType": "PHONE_NUMBER|EMAIL_ADDRESS|PAY_ID",
  "amount": "decimal",
  "currency": "string",
  "note": "string",
  "anonymous": "boolean"
}
```

**Parameters:**
- `identifier` (string, required): Recipient identifier (phone, email, or pay ID)
- `identifierType` (string, required): Type of identifier
  - `PHONE_NUMBER`: Phone number format
  - `EMAIL_ADDRESS`: Email address
  - `PAY_ID`: VALR Pay ID
- `amount` (decimal, required): Amount to send
- `currency` (string, required): Currency code
- `note` (string, optional): Payment note (max 100 chars)
- `anonymous` (boolean, optional): Send anonymously (default: false)

**Response:** Payment details including:
- Transaction ID
- Status
- Timestamp

**Documentation:** https://docs.valr.com/#3b9fbd53-cf42-419d-9fd2-971c14b189b2

---

### Get Payment Limits
**GET** `/v1/pay/limits`

Get current payment limits for VALR Pay.

**Authentication:** Required

**Response:** Payment limits including:
- Daily limit
- Per-transaction limit
- Remaining available amount

**Documentation:** https://docs.valr.com/#a6fbee20-b283-4693-91f1-7a35e81deee8

---

### Get Pay ID
**GET** `/v1/pay/payid`

Get your VALR Pay ID.

**Authentication:** Required

**Response:** Your unique Pay ID

**Documentation:** https://docs.valr.com/#16783a18-d56c-48c3-a491-1db883df43b8

---

### Get Payment History
**GET** `/v1/pay/history`

Get your VALR Pay transaction history.

**Authentication:** Required

**Query Parameters:**
- `skip` (integer, optional): Number to skip (0-100)
- `limit` (integer, optional): Number to return (1-100, default: 50)

**Response:** Array of payment history records including:
- Transaction ID
- Amount and currency
- Recipient/sender
- Timestamp
- Status
- Note

**Documentation:** https://docs.valr.com/#04abb3b3-6c1e-49fd-b599-5e04fb8d204a

---

### Get Payment Details by Identifier
**GET** `/v1/pay/identifier/:identifier`

Get payment details by identifier.

**Authentication:** Required

**Path Parameters:**
- `identifier` (string, required): Payment identifier

**Response:** Payment details

**Documentation:** https://docs.valr.com/#cf7e6c2d-cab7-4ee0-b895-e413f3210efb

---

### Get Payment Status by Transaction ID
**GET** `/v1/pay/transactionid/:transactionId`

Get payment status by transaction ID.

**Authentication:** Required

**Path Parameters:**
- `transactionId` (string, required): Transaction ID

**Response:** Payment status details

**Documentation:** https://docs.valr.com/#cf7e6c2d-cab7-4ee0-b895-e413f3210efb

---

## WebSocket Streams

Real-time data streaming via WebSocket connections.

### Connection Information

**Spot Trading WebSocket:** `wss://api.valr.com/ws/trade`
**Account WebSocket:** `wss://api.valr.com/ws/account`

WebSocket connections require authentication via the initial handshake for authenticated streams.

---

### Spot Streams

Real-time market data for spot trading.

#### Subscribe to Aggregated Order Book Updates

Stream real-time updates to the aggregated order book.

**Stream:** Spot Trading WebSocket

**Subscription Message:**
```json
{
  "type": "SUBSCRIBE",
  "subscriptions": [
    {
      "event": "AGGREGATED_ORDERBOOK_UPDATE",
      "pairs": ["BTCZAR", "ETHZAR"]
    }
  ]
}
```

**Update Messages:** Receive order book updates including:
- Currency pair
- Asks updates
- Bids updates
- Sequence number
- Timestamp

---

#### Subscribe to Full Order Book Updates

Stream all individual order updates in the order book.

**Stream:** Spot Trading WebSocket

**Subscription Message:**
```json
{
  "type": "SUBSCRIBE",
  "subscriptions": [
    {
      "event": "FULL_ORDERBOOK_UPDATE",
      "pairs": ["BTCZAR"]
    }
  ]
}
```

**Update Messages:** Individual order additions, updates, and cancellations

---

#### Subscribe to Market Summary Updates

Stream real-time market summary updates.

**Stream:** Spot Trading WebSocket

**Subscription Message:**
```json
{
  "type": "SUBSCRIBE",
  "subscriptions": [
    {
      "event": "MARKET_SUMMARY_UPDATE",
      "pairs": ["BTCZAR", "ETHZAR"]
    }
  ]
}
```

**Update Messages:** Market statistics including:
- Last price
- 24h volume
- 24h high/low
- Price change percentage
- Bid/ask prices

---

#### Subscribe to New Trade Updates

Stream individual trade executions in real-time.

**Stream:** Spot Trading WebSocket

**Subscription Message:**
```json
{
  "type": "SUBSCRIBE",
  "subscriptions": [
    {
      "event": "NEW_TRADE",
      "pairs": ["BTCZAR"]
    }
  ]
}
```

**Update Messages:** Trade details including:
- Price
- Quantity
- Taker side
- Timestamp
- Trade ID

---

#### Subscribe to Trade Bucket Updates

Stream aggregated OHLCV candle data.

**Stream:** Spot Trading WebSocket

**Subscription Message:**
```json
{
  "type": "SUBSCRIBE",
  "subscriptions": [
    {
      "event": "NEW_TRADE_BUCKET",
      "pairs": ["BTCZAR"],
      "bucketPeriodInSeconds": 60
    }
  ]
}
```

**Parameters:**
- `bucketPeriodInSeconds`: Candle interval (60, 300, 3600, etc.)

**Update Messages:** OHLCV data including:
- Open, High, Low, Close prices
- Volume
- Bucket start time
- Number of trades

---

### Account Streams

Real-time updates for your account.

#### Subscribe to Account Updates

Stream all account-related events.

**Stream:** Account WebSocket (Authenticated)

**Subscription Message:**
```json
{
  "type": "SUBSCRIBE",
  "subscriptions": [
    {
      "event": "ACCOUNT_UPDATE"
    }
  ]
}
```

**Event Types:**
- `BALANCE_UPDATE`: Balance changes
- `NEW_TRADE`: Your trades executed
- `ORDER_PROCESSED`: Order created/updated
- `ORDER_FILLED`: Order fully filled
- `ORDER_PARTIALLY_FILLED`: Order partially filled
- `ORDER_CANCELLED`: Order cancelled
- `ORDER_FAILED`: Order failed
- `INSTANT_ORDER_COMPLETED`: Instant order completed
- `NEW_PENDING_RECEIVE`: Incoming deposit detected
- `SEND_STATUS_UPDATE`: Withdrawal status update
- And more...

**Update Messages:** Vary by event type, containing relevant details for each event.

---

## Data Types & Enums

### Order Types

- **LIMIT**: Standard limit order
- **LIMIT_POST_ONLY**: Limit order that will only be added to the order book (not matched immediately)
- **MARKET**: Market order (crypto-to-ZAR pairs only)
- **SIMPLE**: Simple instant buy/sell (similar to market, supports crypto-to-crypto)
- **STOP_LOSS_LIMIT**: Stop-loss limit order (triggered when price drops)
- **TAKE_PROFIT_LIMIT**: Take-profit limit order (triggered when price rises)

### Order Status

- **OPEN**: Order is open and active
- **NEW**: Order just created
- **ACTIVE**: Order is active in the order book
- **PARTIALLY_FILLED**: Order partially executed
- **FILLED**: Order completely filled
- **CANCELLED**: Order cancelled
- **FAILED**: Order failed

### Order Side

- **BUY**: Buy order
- **SELL**: Sell order

### Time In Force

- **GTC** (Good Till Cancelled): Order remains until filled or cancelled
- **FOK** (Fill Or Kill): Must be completely filled immediately or cancelled
- **IOC** (Immediate Or Cancel): Fill as much as possible immediately, cancel remainder

### Transaction Types

- `BLOCKCHAIN_RECEIVE`: Blockchain deposit received
- `BLOCKCHAIN_SEND`: Blockchain withdrawal sent
- `FIAT_DEPOSIT`: Fiat currency deposit
- `FIAT_WITHDRAWAL`: Fiat currency withdrawal
- `REFERRAL_REBATE`: Referral fee rebate
- `REFERRAL_REWARD`: Referral bonus
- `PROMOTION_REWARD`: Promotional reward
- `INTERNAL_TRANSFER`: Transfer between accounts
- `EXCHANGE_FEE`: Trading fee
- `MAKER_FEE`: Maker trading fee
- `TAKER_FEE`: Taker trading fee
- `OFF_CHAIN_BLOCKCHAIN_SEND`: Off-chain withdrawal
- `OFF_CHAIN_BLOCKCHAIN_RECEIVE`: Off-chain deposit

### Payment Identifier Types

- `PHONE_NUMBER`: Recipient's phone number
- `EMAIL_ADDRESS`: Recipient's email address
- `PAY_ID`: VALR Pay ID

### Withdrawal Status

- `PENDING`: Withdrawal pending
- `PROCESSING`: Withdrawal being processed
- `CONFIRMED`: Withdrawal confirmed
- `COMPLETED`: Withdrawal completed
- `FAILED`: Withdrawal failed
- `CANCELLED`: Withdrawal cancelled

---

## Rate Limiting

**Global Rate Limit:** 500 requests per minute per API key

Rate limits are enforced per API key across all endpoints. If you exceed the rate limit, you'll receive a `429 Too Many Requests` response.

**Best Practices:**
- Implement exponential backoff for retries
- Use WebSocket streams for real-time data instead of polling
- Cache public data when possible
- Monitor your rate limit usage

---

## Error Handling

### HTTP Status Codes

- **200 OK**: Successful request
- **400 Bad Request**: Invalid request parameters
- **401 Unauthorized**: Authentication failed
- **403 Forbidden**: Access denied
- **404 Not Found**: Resource not found
- **429 Too Many Requests**: Rate limit exceeded
- **500 Internal Server Error**: Server error
- **503 Service Unavailable**: Service temporarily unavailable

### Error Response Format

```json
{
  "code": "ERROR_CODE",
  "message": "Human readable error message"
}
```

---

## Additional Resources

- **Official Documentation:** https://docs.valr.com/
- **VALR Website:** https://www.valr.com/
- **Support:** support@valr.com

---

## SDK Implementation

This documentation is based on the **Valr.Net** C# SDK implementation, which provides:
- Strongly-typed API clients
- WebSocket stream management
- Automatic authentication
- Rate limiting
- Error handling
- Comprehensive XML documentation

**Valr.Net Repository:** Available in the current codebase

---

**Last Updated:** 2025-11-06
**API Version:** v1
**Documentation Version:** 1.0
