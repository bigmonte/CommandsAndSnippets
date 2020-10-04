# Unit Testing

## Characteristics of a Good Unit Test

* Fast: Should execute quickly (milliseconds)
* Isolated: Not dependent on external factors (such as database, network connections, etc)
* Repeatable: Same result between runs (unless you change something between runs)
* Self-checking: Should require no human to check if whether it has passed or failed
* Timely: Should not take disproportionately long time to run compared with the code being tested.
* Focused: A unit test should only test one thing.

#### Resources
[https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)

[https://xunit.net/docs/shared-context](https://xunit.net/docs/shared-context)

#### Method risk analysis example

| Input           | Inputs of the method                             |
|-----------------|--------------------------------------------------|
| Process         | Description of the method                        |
| Success Outputs | eg. Status Code in case of APIs                  |
| Failure Outputs | Non-success cases                                |
| Save            | Yes/No (Method can change resources?)            |
| Idempotent      | e.g Doing same op can provide different results? |

## Mocking Framework

Description: It allow us to self-contain everything we need in our unit test and adhere to the Isolation principle.

Using Moq

```bash
dotnet add package Moq
dotnet add package AutoMapper.Extensions.DependecyInjection
```