# Change Log

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [Unreleased] - YYYY-MM-DD

### Security

### Deprecated

### Added

### Changed

### Fixed

### Removed


## [1.10.0] - 2019-06-04

### Added

- Payment: Added endpoint SecupayPayout


## [1.9.1] - 2019-03-22

### Added

- Payment: AssignExternalInvoicePdf possibility to transmit some filename
- Payment: Added some missing fields in PaymentTransaction model


## [1.8.1] - 2018-11-01

### Fixed

- Payment: AssignExternalInvoicePdf some serialize warning/error


## [1.8.0] - 2018-09-12

### Added

- Payment: AssignExternalInvoicePdf

### Fixed

- Payment: Cancel returned (in some special cases) always false
- Payment: Capture returned (in some special cases) always false
- Payment: UpdateBasket returned (in some special cases) always false
- Payment: ReverseAccrual returned (in some special cases) always false
- Payment: SetShippingInformation returned (in some special cases) always false

### Removed

- Payment: container into SecupayCreditcard-Model (was not implemented in the api)
- Payment: container into SecupayInvoice-Model (was not implemented in the api)
- Payment: InitSubsequent (was not implemented in the api)
- Payment: UpdateSubscription (was not implemented in the api)


## [1.7.0] - 2018-08-22

### Added

- Payment: Get a list of payment transactions

### Fixed

- Smart.Transaction: cancel returns always false, now it returns the Transaction object (with the changed status)


## [1.6.0] - 2018-06-14

### Added

- Support for TLS 1.2

### Fixed

- Payment: send shipping information / invoice


## [1.5.0] - 2018-06-07

### Added

- Payment: Mixed basket

### Fixed

- Payment: send shipping information / invoice


## [1.4.1] - 2018-02-13

### Added

- Payment.Model.RedirectUrl: "url_push"


## [1.4.0] - 2017-11-08

### Deprecated

- Loyalty: MerchantCard -> PASSCODE_PROTECTION_OPTION_[XYZ]

### Added

- Payment: Mandate
- Container: Mandate
- Loyalty: MerchantCard (CashBalance, BonusBalance)
- Loyalty: MerchantCardsService (ValidateCSC, ValidatePasscode)
- Loyalty: CardGroupsService (CheckPasscodeEnabled)

### Fixed

- Payment: Wrong payment attribute (TransId, OrderId)


## [1.3.0] - 2017-10-20

### Deprecated

- Payment.Model.Container: The field "Merchant" is now deprecated, because it was not processed by the API

### Added

- Dependency: Microsoft.CSharp
- Smart.Transaction: New field "receipt_merchant" (Händlerbeleg)
- Payment: Add missing payment params
- Payment: Add push notification handling (PaymentEventHandler)

### Changed

- Payment.Model.Basket: Renamed constants to be compatible with the codestyle checker

### Fixed

- Smart.Transaction: Before using EndOfDay there is now a check if STOMP is enabled
- Payment.Model.Basket: Fixed wrong json naming of some params

### Removed

- DemoApp: Moved from secucard/secucard-connect-net-sdk to secucard/secucard-connect-net-sdk-demo


## [1.2.0] - 2017-07-13

### Changed

- DataStorage variable is now protected to be accessible


## [1.1.1] - 2017-06-16

### Fixed

- Smart.Transaction: Cancel payment transaction call


## [1.1.0] - 2017-06-07

### Added

New Methods:

- Smart.Transaction: Fetch an End of Day Report (Kassenschnitt)
- Smart.Transaction: Start extended diagnostic analysis
- Smart.Transaction: Cancel payment transaction (different from Loyalty)

### Fixed

- Wrong encoding of umlauts

### Removed

- Moved license information from *.cs files to README.md and LICENSE.md
- Tests from the SDK


## [1.0.0] - 2017-03-31

### Added

- Live and demo url examples to connect to STOMP
- Changelog file
- Client Payments demo from php-sdk-demo
- Missing attributes in class files

### Changed

- It is no longer possible to set the pin via PUT request. The user have to visit an URL and enter the code (example adjusted)
- If a STOMP message arrives the body will also be displayed
- If an error occurs during the STOMP connection process _isconnected is set to false and an Exception will be raised
- STOMP will only be used if the "Stomp.Enabled" property is set to true in the SecucardConnect.config file
- Examples are separated into folders

### Removed

- Contributing point in Readme.md
- Tests from the SDK


## [0.2.2] - 2017-01-09

### Added

- SecupayPrepaysService new method: Cancel()

### Changed

- Rename license file


## [0.2.1] - 2016-03-21

### Changed

- Assembly Tags


## [0.2.0] - 2016-03-21

First release


[0.2.1]: https://github.com/secucard/secucard-connect-net-sdk/compare/0.2.0...0.2.1
[0.2.2]: https://github.com/secucard/secucard-connect-net-sdk/compare/0.2.1...0.2.2
[1.0.0]: https://github.com/secucard/secucard-connect-net-sdk/compare/0.2.2...1.0.0
[1.1.0]: https://github.com/secucard/secucard-connect-net-sdk/compare/1.0.0...1.1.0
[1.1.1]: https://github.com/secucard/secucard-connect-net-sdk/compare/1.1.0...1.1.1
[1.2.0]: https://github.com/secucard/secucard-connect-net-sdk/compare/1.1.1...1.2.0
[1.3.0]: https://github.com/secucard/secucard-connect-net-sdk/compare/1.2.0...1.3.0
[1.4.0]: https://github.com/secucard/secucard-connect-net-sdk/compare/1.3.0...1.4.0
[1.4.1]: https://github.com/secucard/secucard-connect-net-sdk/compare/1.4.0...1.4.1
[1.5.0]: https://github.com/secucard/secucard-connect-net-sdk/compare/1.4.1...1.5.0
[1.6.0]: https://github.com/secucard/secucard-connect-net-sdk/compare/1.5.0...1.6.0
[1.7.0]: https://github.com/secucard/secucard-connect-net-sdk/compare/1.6.0...1.7.0
[1.8.0]: https://github.com/secucard/secucard-connect-net-sdk/compare/1.7.0...1.8.0
[1.8.1]: https://github.com/secucard/secucard-connect-net-sdk/compare/1.8.0...1.8.1
[1.9.1]: https://github.com/secucard/secucard-connect-net-sdk/compare/1.8.1...1.9.1
[1.10.0]: https://github.com/secucard/secucard-connect-net-sdk/compare/1.9.1...1.10.0

