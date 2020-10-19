# Got the Time

Demos and sample for my "Got the Time" talk

## Requirements

Visual Studio 2019 with .NET Core 3.x support (Community Edition is supported), available for download at https://www.visualstudio.com/downloads/

## Demo: Overdue Invoices

This sample demos how to use [NServiceBus sagas](https://docs.particular.net/nservicebus/sagas/) to handle invoices lifecycle. The sample is self contained and has no external infrastructure dependencies.

## Demo: Order Discount

This sample demos how to use [NServiceBus sagas](https://docs.particular.net/nservicebus/sagas/) to create a never ending business process that handles order discounts. The sample is self contained and has no external infrastructure dependencies.

### A note on LearningTransport and LearningPersistence

The [Learning Transport](https://docs.particular.net/transports/learning/) and [Learning Persistence](https://docs.particular.net/persistence/learning/) are not for production use. They are designed for short term learning and experimentation purposes. It should also not be used for longer-term development, i.e. the same transport and persistence used in production should be used in development and debug scenarios. Select a production transport and persistence before developing features. Do not use the learning transport or learning persistence to perform any kind of performance analysis.
