# RoboRun
### WPF grafikus felületű alkalmazás - egyetemi beadandó feladat

## Feladat
Készítsünk programot, amellyel a következő játékot játszhatjuk.

Adott egy 𝑛 × 𝑛 mezőből álló játékpálya, amelyben egy elszabadult robot bolyong,
és a feladatunk az, hogy betereljük a pálya közepén található mágnes alá, és így
elkapjuk.

A robot véletlenszerű pozícióban kezd, és adott időközönként lép egy mezőt
(vízszintesen, vagy függőlegesen) úgy, hogy általában folyamatosan előre halad
egészen addig, amíg falba nem ütközik. Ekkor véletlenszerűen választ egy új
irányt, és arra halad tovább. Időnként még jobban megkergül, és akkor is irányt
vált, amikor nem ütközik falba.

A játékos a robot terelését úgy hajthatja végre, hogy egy mezőt kiválasztva falat
emelhet rá. A felhúzott falak azonban nem túl strapabíróak. Ha a robot ütközik a
fallal, akkor az utána eldől. A ledőlt falakat már nem lehet újra felhúzni, ott a
robot később akadály nélkül áthaladhat.

A program biztosítson lehetőséget új játék kezdésére a pályaméret megadásával
(7 × 7, 11 × 11, 15 × 15), valamint játék szüneteltetésére (ekkor nem telik az idő,
nem lép a robot, és nem lehet mezőt se kiválasztani). Ismerje fel, ha vége a
játéknak, és jelenítse meg, hogy milyen idővel győzött a játékos. A program játék
közben folyamatosan jelezze ki a játékidőt. Ezen felül szüneteltetés alatt legyen
lehetőség a játék elmentésére, valamint betöltésére

## Közös követelmények:
- A beadandók dokumentációból, valamint programból állnak, utóbbi csak a megfelelő 
dokumentáció bemutatásával értékelhető. Csak funkcionálisan teljes, a feladatnak 
megfelelő, önállóan megvalósított, személyesen bemutatott program fogadható el.
- A megvalósításnak felhasználóbarátnak, és könnyen kezelhetőnek kell lennie. 
A szerkezetében törekednie kell az objektumorientált szemlélet megtartására.
- A programot MVVM architektúrában kell felépíteni, amelyben a megjelenítés rétege
elkülönül a modellnézettől, a modelltől, valamint az adatkezeléstől. A modell és az
adatkezelés nem tartalmazhat semmilyen grafikus felületbeli osztályra történő hivatkozást, 
csak eseményeket küldhet a nézetmodellnek. A nézetmodell nem tartalmazhat semmilyen
játékbeli adatot, a nézet pedig semmilyen háttérkódot.
- A modell működését egységtesztek segítségével kell ellenőrizni. Nem kell teljes körű
tesztet végezni, azonban a lényeges funkciókat, és azok hatásait ellenőrizni kell. 
Az adatbetöltés/mentés teszteléséhez a perzisztencia működését szimulálni kell.
- A dokumentációnak jól áttekinthetőnek, megfelelően formázottnak kell lennie, 
tartalmaznia kell a fejlesztő adatait, a feladatleírást, a feladat elemzését, 
felhasználói eseteit (UML felhasználói esetek diagrammal), a program szerkezetének 
leírását (UML osztálydiagrammal), valamint a tesztesetek leírását. A dokumentáció ne
tartalmazzon kódrészleteket, illetve képenyőképeket. A megjelenő diagramokat megfelelő
szerkesztőeszköz segítségével kell előállítani. A dokumentációt elektronikusan, PDF 
formátumban kell leadni.

## Elemzés
- A játékot három pályamérettel játszhatjuk: (7 × 7), (11 × 11), (15 × 15). 
A program indításkor (11 x 11)-es méretet állít be, és automatikusan új játékot indít.
- A feladatot egyablakos asztali alkalmazásként Windows Presentation Foundation grafikus felülettel valósítjuk meg.
- Az ablakban elhelyezünk egy menüt a következő menüpontokkal: 
File (Új játék, Játék betöltése, Játék mentése, Kilépés), Beállítások ((7 × 7), (11 × 11), (15 × 15)).
Az ablak alján megjelenítünk egy státuszsort, amely a hátralévő időt jelzi.
- A játéktáblát a beállításoknak megfelelő méretű nyomógombokból álló rács reprezentálja.
A nyomógomb egérkattintás hatására lerak egy falat az adott területre. Falat a robot vagy egy már lent lévő fal
pozíciójára nem lehet lerakni. Lent lévő fal pozíciójára később, esetleges ledőlés esetén sem lehet már új falat lerakni.
- A játék automatikusan feldob egy dialógusablakot, amikor vége a játéknak (sikerült beterelni a robotot a pálya közepén
található mágnes alá). Szintén dialógusablakkal végezzük el a mentést, illetve betöltést is, a fájlneveket a felhasználó adja meg.
