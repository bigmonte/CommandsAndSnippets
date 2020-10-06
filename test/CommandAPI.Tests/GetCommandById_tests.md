| Test ID  | Condition                                     | Assert                         |
|----------|-----------------------------------------------|--------------------------------|
| Test 2.1 | Resource ID is invalid (does not exist in DB) | 404 Not Found HTTP Response    |
| Test 2.2 | Resource ID is valid (exists in DB)           | 200 Ok HTTP Response           |
| Test 2.3 | Resource ID is valid (exists in DB)           | Correct Resource Type Returned |