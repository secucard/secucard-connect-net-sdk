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
