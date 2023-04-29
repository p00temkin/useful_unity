## useful_unity

Collection of useful unity classes

| ![alt text](https://github.com/p00temkin/useful_unity/blob/master/img/useful_unity.png?raw=true) |
| :--: |

### JsonRestClient.cs

- Instantiate a restclient in Unity by creating an empty object in your scene and attach the JsonRestClient.cs script.
- Reference the restclient from your trigger class:

 ```
 public JsonRestClient restClient;
 ```

- Define a class for the expected JSON reply, such as RestResponse.cs:

```
 using UnityEngine;
 using System.Collections;

 [System.Serializable]
 class RestResponse
 {
  public string key1;
  public string key2;
 }
```

- Then trigger a REST GET call by running

```
 restClient.Get<RestResponse>(
 "https://somehost/api/v1/restendpoint",
 "onSuccess",
 "onError"
 );
```

The REST response can then be handled in any child script which is named 'onSuccess' or 'onError' since the JsonRestclient uses BroadcastMessage() to the defined methods:

 ```
 private void onSuccess(RestResponse resp)
 {
 Debug.Log("key1 value: " + resp.key1);
 }

 private void onError(string error)
 {
 Debug.Log("onError: " + error);
 }
 ```

### Support/Donate

To support this project directly:

```
   Ethereum/EVM: forestfish.x / 0x207d907768Df538F32f0F642a281416657692743
   Algorand: forestfish.x / 3LW6KZ5WZ22KAK4KV2G73H4HL2XBD3PD3Z5ZOSKFWGRWZDB5DTDCXE6NYU
```

Or please consider donating to EFF:
[Electronic Frontier Foundation](https://supporters.eff.org/donate)
