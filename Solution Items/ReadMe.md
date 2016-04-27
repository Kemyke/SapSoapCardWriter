# Telepítési útmutató



* A telepítést a SapSoapCardWriter.Setup.msi futtatásával végezhetõ el. Itt választható ki, hogy a telepítõ hova másolja fel a futtatáshoz szükséges fájlokat. 

* A telepítés végeztével egy új windows service települ fel SapSoarCardWriter néven. 

* Az alkalmazás beállításai a telepítési mappa alatt lévõ konfigurációs állományokban található

  * SapSoapCardWriter.ServiceHost.exe.config: az alkalmazás fõ konfigurációs állománya. Itt szabályozhatóak a szolgáltatás végpont tulajdonságai. Alapértelmezetten http protokollon figyel, a 10132 -es porton. A WSDL ez alapján lekérdezhetõ a http://localhost:10132/CardWriterService/?wsdl címen.
 
 * Configs/log4net.config: a logolás beállításai. Alaértelmezetten a Logs\Debug.txt fájlba logol napi rotálással.

  * Configs/Unity.config: a dependency injection framewowrk beállításai. Ezeket a beállításokat üzemeltetés szintjén nem javasolt átállítani.

  * Config/SapSoapCardWriterConfig.config: a titkosító alkalmazás saját beállításai, ez jelenleg nincs használatban.

* A szolgáltatás telepítés után nem indul el automatikusan, kézzel kell elindítani. A gép újraindítása esetén automatikusan elindul.

* Alapértelmezetten a Network Service felhasználó nevében kerül futtatásra az alkalmazás. Fontos, hogy a futtató felhasználónak legyen joga megnyitni a konfigurációban beállított portot. (netsh http add urlacl)

* Amint a szolgáltatás elindult, a titkosító szolgáltatás szabványos SOAP üzeneteken keresztül megszólítható (Encrypt, Decrypt metódusok)