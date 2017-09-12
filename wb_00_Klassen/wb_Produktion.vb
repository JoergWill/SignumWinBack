Public Class wb_Produktion

    ''' <summary>
    ''' Enthält den komplette Produktionsplan als Liste von Produktionschritten (wb_Produktionsschritt).
    ''' Jeder Produktionschritt hat Parent und Child. Die Produktion beginnt am Knoten(0) ohne Parent.
    ''' 
    '''     Schritt 0                                            (Child = Schritt 1, Schritt 2...)
    '''         +   Schritt 1               (Parent = Schritt 0)
    '''         +   Schritt 2               (Parent = Schritt 0)
    '''         +   Schritt 3               (Parent = Schritt 0) (Child = Schritt 3.1, Schritt 3.2)
    '''                 +   Schritt 3.1     (Parent = Schritt 3)
    '''                 +   Schritt 3.2     (Parent = Schritt 3)
    '''         +   Schritt 4               (Parent = Schritt 0)
    '''         + ...
    '''         
    ''' Die Anzeige erfolgt im VirtualTree direkt mit der Angabe des Root-Nodes
    ''' </summary>

    Private _RootProduktionsSchritt As New wb_Produktionsschritt(Nothing, "")
    Private _ProduktionsSchritt As wb_Produktionsschritt
    Private _AuftragsNummer As String
    Private _Typ As String
    Private _ArtikelNummer As String
    Private _RezeptNummer As String
    Private _RezeptNr As Integer
    Private _RezeptVar As Integer


    ''' <summary>
    ''' Erster (unsichtbarer) Produktions-Schritt (Root-Node)
    ''' </summary>
    ''' <returns>wb_RezeptSchritt - Root-Node des Rezeptes</returns>
    Public ReadOnly Property RootRezeptSchritt As wb_Produktionsschritt
        Get
            Return _RootProduktionsSchritt
        End Get
    End Property


    ''' <summary>
    ''' Liest alle Datensätze aus wbdaten zur angegeben Tageswechselnummer sortiert nach Produktionsdatum ein 
    ''' </summary>
    ''' <param name="TwNr">Integer Tageswechsel-Nummer</param>
    Public Function MySQLdbSelect_ArbRzSchritte(TwNr As Integer)
        'Datenbank-Verbindung öffnen - MySQL
        Dim winback = New wb_Sql(wb_Konfig.SqlConWbDaten, wb_Sql.dbType.mySql)
        Dim Value As Object
        Dim Typ As Type
        Dim sql As String


        'Abfrage nach Tageswechsel-Nummer
        sql = wb_Sql_Selects.setParams(wb_Sql_Selects.bakArbRezepte, TwNr)

        'Datensätze aus Tabelle Rezeptschritte lesen
        If winback.sqlSelect(sql) Then
            If winback.Read Then
                'Schleife über alle Datensätze
                Do
                    For i = 0 To winback.MySqlRead.FieldCount - 1
                        'Felder mit Typ DateTime müssen speziell eingelesen werden
                        If winback.MySqlRead.GetFieldType(i).Name = "DateTime" Then
                            Value = winback.MySqlRead.GetMySqlDateTime(i)
                        Else
                            Value = winback.MySqlRead.GetValue(i)
                        End If
                        'Felder einlesen
                        MySQLdbRead_Fields(winback.MySqlRead.GetName(i), Value)
                    Next
                    'Chargen mit gleicher Artikel/Rezeptnummer zusammenfassen
                    PrintValues()
                Loop While winback.MySqlRead.Read

                'alle Datensätze eingelesen
                winback.Close()
                Return True
            End If
        End If
            winback.Close()
        Return False
    End Function

    ''' <summary>
    ''' Aufteilen des SQL-Resultset nach Spalten-Namen auf die Obejkt-Eigenschaften
    ''' </summary>
    ''' <param name="Name">String - Spalten-Name aus Datenbank</param>
    ''' <param name="Value">Object - Wert aus Datenbank</param>
    ''' <returns></returns>
    Private Function MySQLdbRead_Fields(Name As String, Value As Object)
        'DB-Null aus der Datenbank
        If IsDBNull(Value) Then
            Value = ""
        End If

        'Feldname aus der Datenbank
        Debug.Print("Feldname/Wert " & Name & "/" & Value.ToString)
        Try
            Select Case Name

                'Produktionsauftrags-Nummer
                Case "B_ARZ_Best_Nr"
                    _AuftragsNummer = Value
                'Typ (Artikel oder Rezept-Zeile)
                Case "B_ARZ_Typ"
                    _Typ = Value
               'Artikelnummer(alpha)
                Case "B_ARZ_KA_NrAlNum"
                    _ArtikelNummer = Value
               'Rezeptnummer(alpha)
                Case "B_RZ_Nr_AlNum"
                    _RezeptNummer = Value
                'Rezeptnummer(intern)
                Case "B_ARZ_Nr"
                    _RezeptNr = Value
                'Rezeptvariante - wird auf 1 gesetzt falls keine Variante angeben ist
                Case "B_ARZ_RZ_Variante_Nr"
                    If Value = 0 Then
                        _RezeptVar = 1
                    Else
                        _RezeptVar = Value
                    End If



            End Select
        Catch ex As Exception
        End Try
        Return True

    End Function

    Private Sub PrintValues()
        Debug.Print("Read BAK_ArbRezepte - Auftragsnummer " & _AuftragsNummer)
        Debug.Print("                    - _Artikelnummer " & _ArtikelNummer)
        Debug.Print("                    - _Rezeptnummer  " & _RezeptNummer)
        Debug.Print("                    - _RzNr          " & _RezeptNr)
        Debug.Print("                    - _RzptVariante  " & _RezeptVar)
    End Sub
End Class

'     // Flag Vorproduktion initialisieren
'      VorProduktion := false;
'      AuftragsNr := '';
'      Try
'        // Produktionsauftrags-Nummer
'        AuftragsNr := AQArbRezepte.fieldbyName('B_ARZ_Best_Nr').Value;
'        // wenn die Auftrags-Nummer mit 'VP' beginnt war dies eine Vorproduktions-Charge
'        If leftStr(AuftragsNr, 2) = 'VP' then
'          begin
'            // eingelesene Charge war eine Vorproduktion
'            VorProduktion := true;
'            // die ersten 2 Zeichen löschen -> Auftragsnummer gleich interne Komponenten-Nummer
'            Delete(AuftragsNr, 1, 2);
'          End
'Else
'AuftragsNr := '';

'      except;
'      End;

'      // Artikel oder Rezept-Zeile
'      Typ := AQArbRezepte.fieldbyName('B_ARZ_Typ').Value;
'      // Artikelnummer(alpha)
'      ArtikelNr := AQArbRezepte.fieldbyName('B_ARZ_KA_NrAlNum').Value;
'      // Rezeptnummer(alpha)
'      Al_RezNr := AQArbRezepte.fieldbyName('B_RZ_Nr_AlNum').Value;
'      // Rezeptnummer(intern)
'      RezeptNr := AQArbRezepte.fieldbyName('B_ARZ_Nr').Value;
'      // Rezeptvariante
'      RezeptVar := AQArbRezepte.fieldbyName('B_ARZ_RZ_Variante_Nr').Value;
'      // wenn keine Rezeptvariante angegeben ist - Variante 1
'      If RezeptVar = '0' then
'        RezeptVar := '1';
'      // Linie
'      Linie := AQArbRezepte.fieldbyName('B_ARZ_LiBeh_Nr').Value - 100;

'      // Schleife über alle ausgewählten Chargen
'      repeat
'begin
'If Typ = '1' then
'            // Sollwert der Charge In kg
'            Sollwert := AQArbRezepte.fieldbyName('B_ARZ_Sollmenge_kg').Value
'          Else
'            // Sollwert der Charge in Stück
'            Sollwert := AQArbRezepte.fieldbyName('B_ARZ_Sollmenge_stueck').Value;

'          // dyn. Array vergrößern
'          l := Length(ChargenSollwerte);
'          SetLength(ChargenSollwerte, l + 1);
'          // Sollwert der Charge
'          ChargenSollwerte[l]:= StrToFloatDef(Sollwert,0,wbdm.wbFormat);

'          // Prüfen ob Rezeptnummer noch identisch
'          // Nächster Datensatz
'          AQArbRezepte.Next;

'        End; // While Not eof()
'      until AQArbRezepte.eof Or (RezeptNr <> AQArbRezepte.fieldbyName('B_ARZ_Nr').Value);

'    // Arbeitsrezepte erzeugen
'    If Typ = '1' then
'      // Arbeitsrezept ohne Artikelnummer - Mengen In kg
'      AddnewBatch('',Al_RezNr,RezeptNr,RezeptVar,IntToStr(Linie),AuftragsNr,Startzeit,false,VorProduktion,0)
'    else
'      // Arbeitsrezept mit Artikelnummer - Mengen in Stück
'      AddnewBatch(ArtikelNr, Al_RezNr, RezeptNr, RezeptVar, IntToStr(Linie), AuftragsNr, Startzeit, False, VorProduktion, 0);

'    // Array Sollwerte wieder löschen
'    SetLength(ChargenSollwerte, 0);

'  End; // If
