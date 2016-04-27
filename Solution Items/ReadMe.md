# Telep�t�si �tmutat�



* A telep�t�st a SapSoapCardWriter.Setup.msi futtat�s�val v�gezhet� el. Itt v�laszthat� ki, hogy a telep�t� hova m�solja fel a futtat�shoz sz�ks�ges f�jlokat. 

* A telep�t�s v�gezt�vel egy �j windows service telep�l fel SapSoarCardWriter n�ven. 

* Az alkalmaz�s be�ll�t�sai a telep�t�si mappa alatt l�v� konfigur�ci�s �llom�nyokban tal�lhat�

  * SapSoapCardWriter.ServiceHost.exe.config: az alkalmaz�s f� konfigur�ci�s �llom�nya. Itt szab�lyozhat�ak a szolg�ltat�s v�gpont tulajdons�gai. Alap�rtelmezetten http protokollon figyel, a 10132 -es porton. A WSDL ez alapj�n lek�rdezhet� a http://localhost:10132/CardWriterService/?wsdl c�men.
 
 * Configs/log4net.config: a logol�s be�ll�t�sai. Ala�rtelmezetten a Logs\Debug.txt f�jlba logol napi rot�l�ssal.

  * Configs/Unity.config: a dependency injection framewowrk be�ll�t�sai. Ezeket a be�ll�t�sokat �zemeltet�s szintj�n nem javasolt �t�ll�tani.

  * Config/SapSoapCardWriterConfig.config: a titkos�t� alkalmaz�s saj�t be�ll�t�sai, ez jelenleg nincs haszn�latban.

* A szolg�ltat�s telep�t�s ut�n nem indul el automatikusan, k�zzel kell elind�tani. A g�p �jraind�t�sa eset�n automatikusan elindul.

* Alap�rtelmezetten a Network Service felhaszn�l� nev�ben ker�l futtat�sra az alkalmaz�s. Fontos, hogy a futtat� felhaszn�l�nak legyen joga megnyitni a konfigur�ci�ban be�ll�tott portot. (netsh http add urlacl)

* Amint a szolg�ltat�s elindult, a titkos�t� szolg�ltat�s szabv�nyos SOAP �zeneteken kereszt�l megsz�l�that� (Encrypt, Decrypt met�dusok)