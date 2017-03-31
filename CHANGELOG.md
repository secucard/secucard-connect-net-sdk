# Change Log

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

## [0.2.1] - 2016-03-21

## [0.2.0] - 2016-03-21