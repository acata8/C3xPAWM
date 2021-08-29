# C3xPAWM

## TECNOLOGIE UTILIZZATE 

Per la creazione di un’applicazione web dinamica è stato utilizzato C#, tramite il framework ASP.NET Core 5.0 e basandosi sull’architettura MVC. 
Nell’implementazione dell’interfaccia utente sono stati utilizzati:
•	Bootstrap 4, per rendere fruibile l’applicazione sia in visualizzazione web sia su mobile;
•	Javascript, per migliorare l’esperienza utente.

La tecnologia scelta per la persistenza dei dati è SqLite poiché offre:
  1.	performance ottime;
  2.	semplicità e leggerezza;
  3.	caricamento del database nell’applicazione.

La tecnologia utilizzata per l’accesso al database è Entity Framework Core, ORM per interrogare il database.
L’utilizzo di un ORM aggiunge un piccolo costo computazionale con una grande mole di dati, ma i vantaggi concessi da EF Core sono:
  1.	lavorare con un modello a oggetti;
  2.	query fortemente tipizzate;
  3.	migliore disaccoppiamento, le query rimarranno uguali per qualsiasi DBMS utilizzeremo;
  4.	facilità nel persistere le modifiche e relazioni;
  5.	possibilità di inviare query SQL in determinati casi.

Servizi e librerie utilizzate:
  - Asp.Net Core Identity, API che supporta le funzionalità legate al login come gestione utenti, gestione password, ruoli, claims, token e altro;
  -	MailKit, libreria per il servizio di mailing;
  -	FluentValidation, libreria per validazione degli input dell’utente;
  -	Mailtrap.io, come SMTP per il testing del servizio di mailing;
  -	Select.HtmlToPdf.NetCore, libreria per convertire un file HTML in pdf;
  - Serilog.AspNetCore, libreria per il logging e utilizzata con:
    -	Serilog.Sinks.Console, per scrivere il contenuto su Console ;
    -	Serilog.Sinks.File, per tracciare il contenuto su file;
    -	Serilog.Settings.Configuration, per leggere le configurazioni da appsetting.json;


## PANORAMICA PROGETTO
Il progetto si rivolge ai centri abitati medi della provincia italiana dove le attività commerciali del centro soffrono la concorrenza di grossi centri commerciali situati nelle periferie.
Il progetto si pone dunque come obiettivo di ricreare una piattaforma da utilizzare principalmente post-vendita, per organizzare la presa in carico e la consegna del pacco acquistato dal cliente, ma anche come “vetrina” per i negozi che decidono di iscriversi e partecipare attivamente alla piattaforma.


## MANUALE UTENTE

  Funzionalità standard, per utenti registrati e non, messe a disposizione:
  - accesso alla home, dove sono presenti i negozi pubblicizzati che hanno deciso di attivare una promozione per un limitato periodo;
  -	elenco dei negozi che sono registrati sulla piattaforma (filtrabile per nome e per città, alfabeticamente ordinabile per tipologia).

  L’utente non registrato non potrà beneficiare della piattaforma per la ricezione degli ordini.
  Alla registrazione sarà possibile identificarsi con una delle tre tipologie disponibili:
  -	Utente base
  -	Commerciante
  -	Corriere


  La registrazione si terminerà con un’e-mail di conferma.
  Al login, sarà possibile richiedere una nuova mail di conferma o di recuperare la password.

### Utente base
  -	visualizzare l’elenco degli ordini effettuati con informazioni relative come nome del corriere, stato del pacco e data di arrivo o arrivo previsto;
  -	gestione dei dati (compresa eliminazione ed esportazione) ed attivazione di autenticazione a due fattori.
  
  
Iscrivendosi al sistema come Commerciante o Corriere, si potrà usufruire della piattaforma come un normale utente base ma si avranno ulteriori funzionalità.

###  Commerciante
Tramite una dashboard, potrà:
  -	modificare i dati del negozio;
  -	attivare una pubblicità;
  -	creare un nuovo ordine;
  -	visualizzare la cronologia di tutti ordini effettuati, dove potrà esportare l’etichetta relativa al pacco.(Esportazione disponibile solo Desktop)

### Corriere
Tramite una dashboard, avrà la possibilità di 
  -	modificare i dati del servizio offerto;
  -	visualizzare l’elenco degli ordini non assegnati a nessun corriere e prenderli in carico;
  -	visualizzare l’elenco degli ordini presi in carico e consegnarli;
  -	visualizzare la cronologia degli ordini consegnati. 


### Admin
Per la gestione della piattaforma è stata pensata la figura dell’admin.
Avrà il potere di:
  -	visualizzare l’elenco di tutti gli utenti iscritti alla piattaforma (filtrabile per e-mail);
  -	visualizzare la lista dei corrieri in attività (filtrabile per nominativo); 
  -	visualizzare la lista dei negozi in attività (filtrabile per nome del negozio);
  -	revocare un ruolo ad un utente specifico, al momento della revoca del ruolo un Commerciante o Corriere diventerà un utente base con funzionalità limitate. I pacchi presi in carico dal relativo corriere verranno rilasciati;
  -	assegnare un ruolo ad un utente specifico, sarà in grado di attivare una nuova attività.

