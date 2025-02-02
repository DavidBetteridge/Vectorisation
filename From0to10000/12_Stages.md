# Stages


```mermaid
%%{init: {"flowchart": {"htmlLabels": false}} }%%
flowchart LR
s1["Source Code"]
s2["Lowered C#"]
s3["IL"]
s4["Assembly"]
s1 --> s2
s2 --> s3
s3 --> s4
```


Tools -> IL Viewer 