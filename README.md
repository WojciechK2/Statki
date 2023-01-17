# StatkiSilnik  

Testowane umieszczanie statków na planszy at random.  
Bez stykania się brzegami czy krawędziami.  
Naiwny algorytm, ale wystarcza na potrzeby projektu.  
Dodatkowo isBoardValid zawiera naiwny algorytm do walidacji wygenerowanej planszym, można użyć do sprawdzania rzmieszczeia statków przez gracza.  
Naiwnie szuka pól które nie są puste następnie sprawdza, czy pola wookół niego są 1. puste, 2. powiązane z tym samym typem statku.  
Placement Tester -> Program.cs to projekt który zawiera metody do zaimplementowania.  

