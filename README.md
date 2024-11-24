# Backend_test_CoolShop

## Request
Si implementi uno script da eseguire da linea di comando, utilizzando a piacimento uno tra NodeJs e C#.

Lo script riceve in input il percorso di un file csv da utilizzare, contenente la lista di ordini di un sito ecommerce.

Es. file csv
Id,Article Name,Quantity,Unit price,Percentage discount,Buyer
1,Coke,10,1,0,Mario Rossi
2,Coke,15,2,0,Luca Neri
3,Fanta,5,3,2,Luca Neri
4,Water,20,1,10,Mario Rossi
5,Fanta,1,4,15,Andrea Bianchi

Lo script deve dare in output i seguenti dati:
Record con importo totale più alto
Record con quantità più alta
Record con maggior differenza tra totale senza sconto e totale con sconto

## Instructions
C# : Eseguire da terninale con comando : dotnet run .\C#\Test_Backend_Coolshop\Program.cs

node.js : Eseguire da terninale con comando : node node.js ./orders.csv
