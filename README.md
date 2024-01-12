# README #

# Nazwa kursu
Testowanie i Jakość Oprogramowania (Projekt)

# Autorzy
Jakub Rodak

# Temat projektu
Testowanie fragmentu kodu pracy inżynierskiej - Zaawansowany system ekwipunku w grze 2D

# Opis projektu
Na projekt składa się fragment kodu z pracy inżynierskiej. Projekt na ten moment(praca inżynierska) nie jest skończony więc testowane będzie to co udało mi się do tej pory zrobić. Po rozpoczęciu rozgrywki gracz ma możliwość podnieść kilka przedmiotów a także założyć je na siebie. Możemy tutaj także spotkać przedmioty użytkowe(woda, jedzenie) które po prostu możemy użyć. W przyszłości zwiększą one nasze punkty zdrowia. Gracz ma także możliwość wyrzucania przedmiotów.

# Uruchomienie projektu
1. Sklonuj repozytorium z githuba
2. Uruchom Unity
3. Otwórz pobrany folder w Unity
4. 

# Uruchomienie testów jednostkowych i integracyjnych


# Scenariusze testowe dla testera manualnego
| Test Case ID | Opis | Kroki testowe | Oczekiwany wynik |
|------------|------------|------------|------------|
| TC_01 | Test sprawdza poprawność movementu. | 1. Uruchom grę  <br/> 2. Za pomocą klawiszy WASD sprawdź poprawność poruszania się po mapie  | Gra powinna umożliwić poprawne poruszanie się po mapie  |
| TC_02 | Test sprawdza poprawność działania kamery | 1. Uruchom grę  <br/> 2. Za pomocą klawiszy WASD poruszaj się po mapie i sprawdzaj czy kamera utrzymuje gracza w zasięgu pola widzenia | Kamera podąża za graczem gdy ten się porusza  |
| TC_03 | Test sprawdza poprawność działania blokerów na granicy mapy. | 1. Uruchom grę <br/> 2. Za pomocą klawiszy WASD spróbuj wyjść poza obszar mapy  | Gracz jest blokowany przez drzewa znajdujące się na mapie i nie może opuścić danego obszaru  |
| TC_04 | Test sprawdza poprawność podnoszenia przedmiotów | 1. Uruchom grę  <br/> 2. Za pomocą klawiszy WASD, poruszając się po mapie stań na  przedmiocie. | Przedmiot przenosi się do ekwipunku  |
| TC_05 | Test sprawdza poprawność podnoszenia przedmiotów po osiągnięciu limitu przedmiotów w ekwipunku | 1. Uruchom grę  <br/> 2. Za pomocą klawiszy WASD, poruszając się po mapie spróbuj zebrać kilkanaście różnych przedmiotów.  | Po zebraniu 9 róźnych przedmiotów, gra blokuje możliwość podnoszenia kolejnych  |
| TC_06 | Test sprawdza poprawność otwierania ekwipunku  | 1. Uruchom grę <br/> 2. Naciśnij przycisk "i"  | Okno ekwipunku poprawnie się otwiera  |
| TC_07 | Test sprawdza poprawność otwierania karty postaci | 1. Uruchom grę  <br/> 2. Naciśnij przycisk "tab"  | Okno karty postaci poprawnie się otwiera  |
| TC_08 | Test sprawdza poprawność zakładania przedmiotów  | 1. Uruchom grę  <br/> 2.  Za pomocą klawiszy WASD, poruszając się po mapie stań na  przedmiocie związanym z pancerzem. <br/> 3. Po zebraniu przedmiotu, za pomocą przycisków "i" oraz "tab" otwórz odpowiednie panele <br/> 4. Naciśnij  na dany element pancerza znajdujący się po prawej stronie ekranu    | Element pancerza poprawnie przenosi się z panelu ekwipunku do panelu ekranu postaci  |
| TC_09 | Test sprawdza poprawność podmieniania przedmiotów  | 1. Uruchom grę  <br/> 2.  Za pomocą klawiszy WASD, poruszając się po mapie stań dwukrotnie na przedmiocie związanym z pancerzem (np. dwa hełmy, lub dwa pancerze) . <br/> 3. Po zebraniu przedmiotów, za pomocą przycisków "i" oraz "tab" otwórz odpowiednie panele <br/> 4. Naciśnij  na dany element pancerza znajdujący się po prawej stronie ekranu  <br/> 5. Ponownie naciśnij na drugi element pancerza znajdujący się po prawej stronie ekranu   | Elementy pancerza poprawnie zamieniają się miejscami. Hełm znajduący się na ekranie postaci trafia do ekwipunku, natomiast ten z ekwipunku przenosi się do ekranu postaci  |
| TC_10 | Test sprawdza poprawność wyrzucania przedmiotu z ekwipunku | 1. Uruchom grę <br/> 2. Za pomocą klawiszy WASD, poruszając się po mapie stań na  przedmiocie <br/> 3. Otwórz panel ekwipunku za pomocą "i" <br/> 4. Przeciągnij przedmiot znajdujący się w panelu ekwipunku na mapę | Przedmiot poprawnię ląduje na mapie opuszczając tym samym panel ekwipunku  |


# Technologie użyte w projekcie
Unity C#, Github
