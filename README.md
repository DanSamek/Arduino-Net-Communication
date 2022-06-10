# Arduino-Net-Communication

## Důvod

V dnešní době je skoro vše na webu a tak jsem se rozhodl vytvořit webovou aplikaci pro arduino, aby šlo ovládat na dálku.

## Využití

Využití je neomezené, stačí trocha představivosti a programování :-)

## Jak to funguje

- z webové stránky (klienta) pošlu data na arduino
- arduino je zpracuje a vykoná funkci
- po vykonání funkce arduino odpoví na server
- server pošle odpověd na klienta


# Logs
zde se zapisují všechny funkce, které byly spuštěny (pouze sendData)

## Jak to spustit?

### Options.csv

do kterého zapíšeme (1 operace = jeden řádek)
- zapíšeme číslo funkce (doporučuji od 0-xx)
- funkci, jakou to má vykonávat (sendData, getData) z názvu vyplývá, co funkce dělá.
- název, který bude viditelný na stránce
- funkce na arduinu
- piny a hodnoty, pořadí může být náhodné, ale toto pořadí je potřeba dodržet v kódu arduina

### Arduino kód (init.ino)

Tady je potřeba trochu dopsat kód pro danou funkci

- do metody Execute() je potřeba dopsat if př.:
if(function == "potenciometr") res=potenciometr(data[1].toInt());
- musí vždy vracet hodnotu, pokud je to sendData, tak return "ok";
- když getData, tak jakoukoliv, ale musí to být ČÍSLO, tyto hodnoty se dávají do grafu.
- v kódu jsou ukázkové funkce (kdyby to nebylo z návodu jasné, kód pomůže).












