# GDi_zadatak


Zadatak je bio pomoći Johnu s aplikacijom za praćenje vozila i vozača.

Ukratko, API je napravljen, frontend NIJE.

Baza: MS SQL Express
API: ASP.NET Core
Frontend: ASP.NET Core (Razorpages)


U solutionu su dva projekta, jedan je API, a drugi je frontend.

Frontend:
probao sam prikazati mapu kao što je zadano u zadatku, ali ne znam se koristiti ovim razorpage-ima.
Probao sam kod u Codepen.io i tamo fino radi, dok u aplikaciji odbija. Pretpostavljam da je problem u mojem ne znanju razorpageova
i njegovih procesa, pa dolazi do problema sa sinkronizacijom (?). Kanio sam dodati barem forme za CRUD operacije, ali ne stignem više
naučiti rad s razorpage-ovima.

API problemi:
1. registracija i godina proizvodnje automobila nisu kontrolirane. Na entitetima su to string i int - znači bilo što. Registracija jedino mora biti unique.
2. nije moguće dodati novi automobil ako se ručno ne dodaju.
3. Neke od podjela odgovornosti su neuniformne na razini Service-Repository
4. Format statusa zahtjeva je, po meni, loš.
5. Kôd nije dokumentiran

Kod dodjele vozila vozačima, unose se dije vrijednosti, ID vozača i/ili ID vozila se mogu postaviti kao -1 što predstavlja unassignement.
Ideja je da se može ili ići logikom: idem osloboditi vozača ili osloboditi vozilo.

U appsettings.json dokumentu API projekta je connectionString za bazu. Postavljeno je da se api vrti na portu 7036 na localhostu.

U soulutionu sam postavio da se pokreće samo API jer ovaj frontend nije vrijedan spomena.
  
    
