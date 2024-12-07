# ServerInfo
Displays a pavlovs server info via ip

When executed it asks vor GameVersion, Ip & UpdateFrequency. (doesnt save)
These options can also be easily set by the .exe args.
Just create a short cut and apend the target parth with up to 3 params.
First param: GameVersion, requird & will ask for it if not set
Second param: Ip, required & will ask for it if not set
Third param: UpdateFrequency(Seconds,double), optional (default = 5 seconds)

Example: 1.0.23 194.242.56.49 1
GameVersion:      "1.0.23"
Ip:              "194.242.56.49"
UpdateFrequency:  "1"
