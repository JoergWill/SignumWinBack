#!/usr/bin/perl -w

#
#  prodimport-sub.pm:  Unterprogramme zu prodimport.pl
#

use strict;

my $log_level = 1;
my $DB_NEU = 2;

   # ---- Konstanten: ----
my $ART_TYP_ARTIKEL = 2;     # Typ 'Artikel' für Feld 'ARZ_Typ'
my $EINHEIT_STUECK = 11;     # Einheit 'Stück' für Feld 'ARZ_Art_Einheit' bei Import Artikelbezogen

my $ART_TYP_REZEPT = 1;      # Typ 'Artikel' für Feld 'ARZ_Typ'
my $EINHEIT_KG = 1;     	 # Einheit 'kg' für Feld 'ARZ_Art_Einheit' bei Import Rezeptbezogen
#TODO diese Konstante in Output-String einpflegen

   # ---- Datenbank-Variablen: ----
my $dbh;

   # ---- Aktueller Artikel: ----
my $art_num_fields;
my $art_fld_names;
my @art_rows;
my $art_anz_rows;
my $art_nr_alnum;
my $art_nr_alnum_alt = "";   # Für Erkennung eines neuen Artikels
my $art_rz_nr;
my $art_stueck_gewicht = 0;
my $Artikel_Bez = "",
my $Artikel_Typ,
my $Artikel_Einheit;
my $art_charge_opt = 0;
my $stueck_charge = 0;
my $prozent_charge= 0;

   # ---- Aktuelles Rezept: ----
my $rz_nr_alnum;
my $rz_variante_nr;
my $rz_variante_nr_alt = "";   # Für Erkennung eines neuen Artikels!! (@V3.1)
my $rz_num_fields;
my $rz_fld_names;
my @rz_rows;
my $rz_anz_rows;
my $rezept_menge;
my $mengen_faktor_real;
my $menge_charge;
my $Rezept_Nr_AlNum;
my $Rezept_Bez;
my $Rezept_Gewicht;

   # ---- Indizes der Felder im Rezept-Array: ----
my $idx__KT_Typ_Nr;
my $idx__RS_Index;
my $idx__RS_Schritt_Nr;
my $idx__RS_Ko_Nr;
my $idx__RS_ParamNr;
my $idx__RS_Wert;
my $idx__RS_Par1;
my $idx__RS_Par2;
my $idx__RS_Par3;

   # 18.04.2006: Indizes für die weiteren Zusatzfelder bei Flat-Produktion:
my $idx__KO_Nr_AlNum;      # für 'ARS_KO_Nr_AlNum'
my $idx__KO_Bezeichnung;   # für 'ARS_KO_Bezeichnung'
my $idx__KO_Temp_Korr;     # für 'ARS_KO_Temp_Korr'
my $idx__KP_Wert;          # für 'ARS_KP_Wert'
# my $idx__KT_Typ_Nr;        # für 'ARS_KT_Typ_Nr'
my $idx__KT_Rezept;        # für 'ARS_KT_Rezept'
my $idx__KT_Bezeichnung;   # für 'ARS_KT_Bezeichnung'
my $idx__KT_KurzBez;       # für 'ARS_KT_KurzBez'
my $idx__KT_EinheitIndex;  # für 'ARS_KT_EinheitIndex'
my $idx__KT_Format;        # für 'ARS_KT_Format'
my $idx__KT_Laenge;        # für 'ARS_KT_Laenge'
my $idx__KT_UnterGW;       # für 'ARS_KT_UnterGW'
my $idx__KT_OberGW;        # für 'ARS_KT_OberGW'
my $idx__E_Einheit;        # für 'ARS_E_Einheit'
# my $idx__RS_Wert;          # für 'ARS_RS_Wert'

   # ---- Zusatz-Array für die ARS-Daten: ----
my @ars_rows;
my $ars_anz_rows;
my $idx__ARS_Wert2 = 0;
my $ars_num_fields = 1;

   # ---- Daten für Aufbau ARZ/ARS: ----
my $rz_liniengruppe;
my $prod_linie;
my $seg_idx;
my $Beh_Nr;
my $next_ARZ_Index;
my $next_ARZ_Charge_Nr;
my $next_ARS_RunIdx;
my $next_ARZ_Art_Index;
my $bestell_nr;

sub  get_Linie_von_Liniengruppe;
# sub  local_Now;


sub  db_connect
{
   my ( $db, $user, $pass ) = @_;
   my ( $dsn ) = sprintf ( 'DBI:mysql:%s', $db );
   # print "db_connect(): dsn = $dsn\n";
   $dbh = DBI->connect ( $dsn, $user, $pass ) || die "Fehler connect(): $DBI::errstr";
}

sub  db_disconnect
{
   # print "db_disconnect()\n";
   $dbh->disconnect();
}


sub  Artikel_und_Rezept_laden
{
   my ( $db_ret ) = 0;
   my ( $art_nr_alnum_, $rz_variante_nr_ ,$rz_nr_alnum) = @_;

   my $neuer_Artikel = 0;
   
   # ----- (@V3.1) Flag neuer Artikel auch bei Änderung der Rezeptvariante
   if (( $art_nr_alnum_  ne  $art_nr_alnum_alt ) or ( $rz_variante_nr_ ne $rz_variante_nr_alt))
   {  $neuer_Artikel = 1;
   }
   if ( $neuer_Artikel == 0 )
   {  goto  ende;
   }

   if ( $log_level >= 1 )
   {  print "Artikel '$art_nr_alnum_' laden ...\n";
   }
   
   # Wenn in der Produktionsliste keine Artikelnummer übergeben wird, muss ein Dummy-Artikel angelegt werden
   if ( $art_nr_alnum_ eq 'K') 
   {
		# Dummy Artikel anlegen
   		$db_ret = Dummy_Artikel_laden ( $rz_nr_alnum );
   }
   else
   {
   		# Artikel suchen und aufloesen
   		$db_ret = Artikel_laden ( $art_nr_alnum_ );
   		print ("Nach Artikel_laden '$art_rz_nr'\n");
   }
   if ( $db_ret < 0 )   { goto  ende; }

   if ( $log_level >= 1 )
   {  print "Rezept zu Artikel '$art_nr_alnum_' laden (Variante $rz_variante_nr_) ...\n";
   }
   $db_ret = Rezept_laden ( $rz_variante_nr_ );
   if ( $db_ret < 0 )   { goto  ende; }

ende:
   return ( $db_ret );
}


sub  Artikel_laden
{
   my ( $db_ret ) = 0;
   my $art_felder = "KO_Nr_AlNum, KO_Bezeichnung";

      # ---- Übergebene Parameter abholen: ----
   my ( $art_nr_alnum_ ) = @_;
      # ---- Lokale Variablen: ----
   my $sql_artikel_laden;

   $sql_artikel_laden = qq
   {
      select * from Komponenten where (KA_Art = 1) and KO_Nr_AlNum = '$art_nr_alnum_'
   };

   my $sth = $dbh->prepare ( $sql_artikel_laden );

   $art_anz_rows = $sth->execute();
   if ( $art_anz_rows == 0 )
   {  print "*** Artikel '$art_nr_alnum_' nicht gefunden ! ***\n";
      $art_nr_alnum_alt = "";
      $db_ret = -1;
      goto  ende;
   }

   my ( $ref, $idx, $zeile, $spalte, $feld_name, $wert );

   $art_num_fields = $sth->{'NUM_OF_FIELDS'};
   $art_fld_names  = $sth->{'NAME'};

      # ---- Artikelvariablen zurücksetzen: ----
   $art_anz_rows = 0;
   $art_rz_nr = 0;

      # ---- Artikel laden: ----
   $zeile = 0;
   while ( $ref = $sth->fetchrow_arrayref )
   {  for ( $spalte = 0; $spalte < $art_num_fields; $spalte++ )
      {  $idx = $zeile * $art_num_fields + $spalte;
         $art_rows[$idx] = $$ref[$spalte];
      }
      $zeile += 1;
   }
   $art_anz_rows = $zeile;
   $sth->finish();

   if ( $log_level >= 2 )
   {  print "   Artikel '$art_nr_alnum_': Anzahl Zeilen/Spalten: $art_anz_rows/$art_num_fields\n";
   }

      # ---- Artikel-Nr. hier in sub1 speichern: ----
   $art_nr_alnum = $art_nr_alnum_;

      # ---- Artikel auswerten ( Daten des 1. Satzes): ----
   for ( $idx = 0;  $idx < $art_num_fields;  $idx++ )
   {  $feld_name = $$art_fld_names[$idx];
         # ---- Interne Rezept-Nr. laden: ----
      if ( $feld_name eq 'KA_RZ_Nr' )
      {  $art_rz_nr = $art_rows[$idx];
   		print ("In Artikel_laden '$art_rz_nr'\n");
      }
         # ---- Stückgewicht laden und umrechnen in kg: ----
      if ( $feld_name eq 'KA_Stueckgewicht' )
      {  $art_stueck_gewicht = to_float ( $art_rows[$idx] ) / 1000.0;
      }
         # ---- Artikel-Bezeichnung laden: ----
      if ( $feld_name eq 'KO_Bezeichnung' )
      {  $Artikel_Bez = $art_rows[$idx];
      }
         # ---- Optimal-Chargengrösse des Artikels laden: ----
      if ( $feld_name eq 'KA_Charge_Opt' )
      {  $art_charge_opt = to_float ( $art_rows[$idx] );
      }

      if ( $log_level >= 3 )
      {  print "      KA_RZ_Nr = '$art_rz_nr'\n";
         print "      KA_Stueckgewicht = '$art_stueck_gewicht'\n";
         print "      KO_Bezeichnung = '$Artikel_Bez'\n";
         print "      KA_Charge_Opt = '$art_charge_opt'\n";
      }
   }

      # ---- Nur Artikel werden importiert: ----
   $Artikel_Typ = $ART_TYP_ARTIKEL;
   $Artikel_Einheit = $EINHEIT_STUECK;

ende:
   return ( $db_ret );
}

sub  Dummy_Artikel_laden
{
   my ( $db_ret ) = 0;
   my $art_felder = "KO_Nr_AlNum, KO_Bezeichnung";

      # ---- Übergebene Parameter abholen: ----
   my ( $art_nr_alnum_ ) = @_;
      # ---- Lokale Variablen: ----
   my $sql_rezeptnr_finden;

   $sql_rezeptnr_finden = qq
   {
      SELECT * FROM Rezepte WHERE RZ_Nr_AlNum = '$art_nr_alnum_'
   };

   my $sth = $dbh->prepare ( $sql_rezeptnr_finden );

   $art_anz_rows = $sth->execute();
   if ( $art_anz_rows == 0 )
   {  print "*** Rezept '$art_nr_alnum_' nicht gefunden ! ***\n";
      $art_nr_alnum_alt = "";
      $db_ret = -1;
      goto  ende;
   }

   my ( $ref, $idx, $zeile, $spalte, $feld_name, $wert );

   $art_num_fields = $sth->{'NUM_OF_FIELDS'};
   $art_fld_names  = $sth->{'NAME'};

      # ---- Artikelvariablen zurücksetzen: ----
   $art_anz_rows = 0;
   $art_rz_nr = 0;

      # ---- Artikel laden: ----
   $zeile = 0;
   while ( $ref = $sth->fetchrow_arrayref )
   {  for ( $spalte = 0; $spalte < $art_num_fields; $spalte++ )
      {  $idx = $zeile * $art_num_fields + $spalte;
         $art_rows[$idx] = $$ref[$spalte];
      }
      $zeile += 1;
   }
   $art_anz_rows = $zeile;
   $sth->finish();

   if ( $log_level >= 2 )
   {  print "   Artikel '$art_nr_alnum_': Anzahl Zeilen/Spalten: $art_anz_rows/$art_num_fields\n";
   }

      # ---- Artikel-Nr. hier in sub1 speichern: ----
   $art_nr_alnum = $art_nr_alnum_;

      # ---- Rezept auswerten ( Daten des 1. Satzes): ----
   for ( $idx = 0;  $idx < $art_num_fields;  $idx++ )
   {  $feld_name = $$art_fld_names[$idx];
         # ---- Interne Rezept-Nr. laden: ----
      if ( $feld_name eq 'RZ_Nr' )
      {  $art_rz_nr = $art_rows[$idx];
      }
         # ---- Stückgewicht laden und umrechnen in kg: ----
      if ( $feld_name eq 'RZ_Gewicht' )
      {  $art_stueck_gewicht = to_float (1);
      }
         # ---- Artikel-Bezeichnung laden: ----
      if ( $feld_name eq 'RZ_Bezeichnung' )
      {  $Artikel_Bez = $art_rows[$idx];
      }
         # ---- Optimal-Chargengrösse des Artikels laden: ----
      if ( $feld_name eq 'RZ_Gewicht' )
      {  $art_charge_opt = to_float ( $art_rows[$idx] );
      }

      if ( $log_level >= 3 )
      {  print "      KA_RZ_Nr = '$art_rz_nr'\n";
         print "      KA_Stueckgewicht = '$art_stueck_gewicht'\n";
         print "      KO_Bezeichnung = '$Artikel_Bez'\n";
         print "      KA_Charge_Opt = '$art_charge_opt'\n";
      }
   }

      # ---- Nur Artikel werden importiert: ----
   $Artikel_Typ = $ART_TYP_REZEPT;
   $Artikel_Einheit = $EINHEIT_KG;
   #TODO Warum wird hier die falsche Einheit in die Tabelle geschrieben?

ende:
   return ( $db_ret );
}
sub  Rezept_laden
{
   my ( $db_ret ) = 0;
   my $rz_felder = "RZ_Nr, RZ_Variante_Nr, RZ_Nr_AlNum, RZ_Bezeichnung,
                    RS_Schritt_Nr, RS_Ko_Nr, RS_ParamNr, RS_Wert";

      # ---- Übergebene Parameter abholen: ----
   my ( $rz_variante_nr_ ) = @_;
      # ---- Lokale Variablen: ----
   my $sql_rezept_laden;

   $sql_rezept_laden = qq
   {
      select  *

      from  Rezepte, RezeptSchritte, Komponenten,
            KomponParams, KomponTypen, Einheiten

               # ---- Links Rezepte --> Rezeptschritte: ----
      where      RS_RZ_Nr          = RZ_Nr
            and  RS_RZ_Variante_Nr = RZ_Variante_Nr

               # ---- Eingrenzen auf Rezept und Rezeptvariante: ----
            and  RS_RZ_Nr          = '$art_rz_nr'
            and  RS_RZ_Variante_Nr = '$rz_variante_nr_'

               # ---- Link RS --> Komponenten: ----
            and  KO_Nr             = RS_Ko_Nr

               # ---- Link Komponenten/RS --> KomponParams: ----
            and  KP_Ko_Nr          = KO_Nr
            and  KP_ParamNr        = RS_ParamNr

               # ---- Link Komponenten/RS --> KomponTypen: ----
            and  KT_Typ_Nr         = KO_Type
            and  KT_ParamNr        = RS_ParamNr

               # ---- Eingrenzen auf Rezeptparameter: ----
            and (KT_Rezept = "R" or KT_Rezept = "X")

               # ---- Link Einheiten --> KomponTypen: ----
            and  KT_EinheitIndex   = E_LfdNr

               # ---- Sortierung: ----
      order by RS_RZ_Nr, RS_Schritt_Nr, RS_ParamNr
   };


   my $sth = $dbh->prepare ( $sql_rezept_laden );

   $rz_anz_rows = $sth->execute();
   if ( $rz_anz_rows == 0 )
   {  print "*** Rezept/Variante $art_rz_nr/$rz_variante_nr_ nicht gefunden ! ***\n";
      $db_ret = -1;
      goto  ende;
   }

   my ( $ref, $idx, $zeile, $spalte, $feld_name, $wert );

   $rz_num_fields = $sth->{'NUM_OF_FIELDS'};
   $rz_fld_names  = $sth->{'NAME'};

      # ---- Rezeptvariablen zurücksetzen: ----
   $rz_anz_rows = 0;
      # ---- Zusatzarray-Variablen zurücksetzen: ----
   $ars_anz_rows = 0;

      # ---- Rezept laden: ----
   $zeile = 0;
   while ( $ref = $sth->fetchrow_arrayref )
   {      # ---- Original-Rezeptfelder: ----
      for ( $spalte = 0; $spalte < $rz_num_fields; $spalte++ )
      {  $idx = $zeile * $rz_num_fields + $spalte;
         $rz_rows[$idx] = $$ref[$spalte];
      }
          # ---- ARS-Zusatzfelder initialisieren: ----
      for ( $spalte = 0; $spalte < $ars_num_fields; $spalte++ )
      {  $idx = $zeile * $ars_num_fields + $spalte;
         $ars_rows[$idx] = 0;
      }

      $zeile += 1;
   }
   $rz_anz_rows = $zeile;
   $sth->finish();

      # ---- Rezeptschritte vorhanden ?: ----
   if ( $rz_anz_rows == 0 )
   {  print "*** Rezept '$art_rz_nr' Variante '$rz_variante_nr_': Keine Rezeptschritte vorhanden ! ***\n";
      $db_ret = -1;
      goto  ende;
   }

   if ( $log_level >= 3 )
   {  print "   Rezept '$art_rz_nr' Variante '$rz_variante_nr_': Anzahl Zeilen/Spalten: $rz_anz_rows/$rz_num_fields\n";
   }

      # ---- Rezeptvariante-Nr hier in sub1 speichern: ----
   $rz_variante_nr = $rz_variante_nr_;

      # ---- Rezeptkopf auswerten (Daten des 1. Satzes): ----
   $rz_liniengruppe = 0;
   for ( $idx = 0;  $idx < $rz_num_fields;  $idx++ )
   {  $feld_name = $$rz_fld_names[$idx];
         # ---- Liniengruppe feststellen: ----
      if ( $feld_name eq "RZ_Liniengruppe" )
      {  $rz_liniengruppe = $rz_rows[$idx];
      }
         # ---- Alphanumerische Rezept-Nr: ----
      if ( $feld_name eq "RZ_Nr_AlNum" )
      {  $Rezept_Nr_AlNum = $rz_rows[$idx];
      }
         # ---- Rezept-Bezeichnung: ----
      if ( $feld_name eq "RZ_Bezeichnung" )
      {  $Rezept_Bez = $rz_rows[$idx];
      }
         # ---- Rezept-Gewicht: ----
      if ( $feld_name eq "RZ_Gewicht" )
      {  $Rezept_Gewicht = $rz_rows[$idx];
      }

      if ( $log_level >= 2 )
      {  print "RZ_Liniengruppe = $rz_liniengruppe\n";
         print "RZ_Nr_AlNum = $Rezept_Nr_AlNum\n";
         print "RZ_Bezeichnung = $Rezept_Bez\n";
      }

      # ---- Öfter benötigte Indizes zu den Rezeptfeldern setzen: ----

      if ( $feld_name eq 'KT_Typ_Nr' )   { $idx__KT_Typ_Nr  = $idx; }

      if ( $feld_name eq 'RS_Index' )      { $idx__RS_Index      = $idx; }
      if ( $feld_name eq 'RS_Schritt_Nr' ) { $idx__RS_Schritt_Nr = $idx; }
      if ( $feld_name eq 'RS_Ko_Nr' )      { $idx__RS_Ko_Nr      = $idx; }

      if ( $feld_name eq 'RS_ParamNr' )  { $idx__RS_ParamNr = $idx; }
      if ( $feld_name eq 'RS_Wert' )     { $idx__RS_Wert    = $idx; }

      if ( $feld_name eq 'RS_Par1' )  { $idx__RS_Par1 = $idx; }
      if ( $feld_name eq 'RS_Par2' )  { $idx__RS_Par2 = $idx; }
      if ( $feld_name eq 'RS_Par3' )  { $idx__RS_Par3 = $idx; }

      if ( $feld_name eq 'KO_Nr_AlNum'    ) { $idx__KO_Nr_AlNum = $idx;    }
      if ( $feld_name eq 'KO_Bezeichnung' ) { $idx__KO_Bezeichnung = $idx; }
      if ( $feld_name eq 'KO_Temp_Korr'   ) { $idx__KO_Temp_Korr = $idx;   }
      if ( $feld_name eq 'KP_Wert'   )      { $idx__KP_Wert = $idx;   }
      # if ( $feld_name eq 'KT_Typ_Nr' )      { $idx__KT_Typ_Nr = $idx; }
      if ( $feld_name eq 'KT_Rezept' )      { $idx__KT_Rezept = $idx; }
      if ( $feld_name eq 'KT_Bezeichnung' ) { $idx__KT_Bezeichnung = $idx;  }
      if ( $feld_name eq 'KT_KurzBez'     ) { $idx__KT_KurzBez = $idx;      }
      if ( $feld_name eq 'KT_EinheitIndex') { $idx__KT_EinheitIndex = $idx; }
      if ( $feld_name eq 'KT_Format'  )     { $idx__KT_Format = $idx;  }
      if ( $feld_name eq 'KT_Laenge'  )     { $idx__KT_Laenge = $idx;  }
      if ( $feld_name eq 'KT_UnterGW' )     { $idx__KT_UnterGW = $idx; }
      if ( $feld_name eq 'KT_OberGW' )      { $idx__KT_OberGW = $idx; }
      if ( $feld_name eq 'E_Einheit' )      { $idx__E_Einheit = $idx; }
      # if ( $feld_name eq 'RS_Wert'   )      { $idx__RS_Wert = $idx;   }
   }


      # ---- Rezept ausgeben: ----
   for ( $zeile = 0; $zeile < $rz_anz_rows; $zeile++ )
   {
      for ( $spalte = 0;  $spalte < $rz_num_fields;  $spalte++ )
      {
         $idx = $zeile * $rz_num_fields + $spalte;
         $wert = $rz_rows[$idx];
         if ( not $wert ) { $wert = ""; }
         $feld_name = $$rz_fld_names[$spalte];
         if ( $log_level >= 3 )
         {
            printf ( "%d %d %s:  '%s'\n", $zeile, $spalte, $feld_name, $wert );
         }

         # $s1 = sprintf ( "%04d%04d%03d%s%03d%s", $zeile, $spalte,
         #        length($feld_name), $feld_name, length($wert), $wert );
      }
   }

ende:
   return ( $db_ret );
}


sub  Indizes_berechnen
{
   my $db_ret = 0;

# ----- (@V3.1) Flag neuer Artikel auch bei Änderung der Rezeptvariante   
   if (( $art_nr_alnum  eq  $art_nr_alnum_alt ) and ($rz_variante_nr eq $rz_variante_nr_alt))
   {
      # ---- Gleicher Artikel ----

# print "Gleicher Artikel:  art_nr_alnum     = $art_nr_alnum\n";
# print "                   art_nr_alnum_alt = $art_nr_alnum_alt\n";

         # ---- Indizes auf Basis der vorhandenen berechnen: ----
      $next_ARZ_Index     += 1;
      $next_ARZ_Charge_Nr += 1;
      $next_ARS_RunIdx    += 1;

      goto  ende;
   }

      # ---- Erst hier nach 'art_nr_alnum_alt' kopieren: ----
   $art_nr_alnum_alt = $art_nr_alnum;
   $rz_variante_nr_alt = $rz_variante_nr;

      # ---- Produktionslinie entsprechend Liniengruppe besorgen: ----
   $db_ret = get_Linie_von_Liniengruppe ( $rz_liniengruppe );
   if ( $db_ret < 0 )  { goto  ende; }
      # ---- Segment zur Linie besorgen: ----
   $db_ret = prod_get_seg_index ( $prod_linie );
   if ( $db_ret < 0 )  { goto  ende; }

      # ---- 'next_ARZ_Index' besorgen: ----
   $db_ret = get_next_ARZ_Index ( $Beh_Nr );
   if ( $db_ret < 0 )  { goto  ende; }
      # ---- 'next_ARZ_Art_Index' besorgen: ----
   $db_ret = get_next_Art_Index ( $Beh_Nr );
   if ( $db_ret < 0 )  { goto  ende; }
      # ---- 'next_ARZ_Charge_Nr' besorgen: ----
   $db_ret = get_next_ARZ_Charge_Nr ( $Beh_Nr );
   if ( $db_ret < 0 )  { goto  ende; }

      # ---- 'next_ARS_RunIdx' besorgen: ----
   $db_ret = get_next_ARS_RunIdx ( $Beh_Nr );
   if ( $db_ret < 0 )  { goto  ende; }
   $next_ARS_RunIdx = start_next_ARS_RunIdx_Bereich ( $next_ARS_RunIdx );

   if ( $log_level >= 5 )
   {  print "Produktions-Linie: prod_linie = $prod_linie\n";
      print "Produktions-Segment: seg_idx = $seg_idx\n";
      print "next_ARZ_Index = $next_ARZ_Index\n";
      print "next_ARZ_Art_Index = $next_ARZ_Art_Index\n";
      print "next_ARZ_Charge_Nr = $next_ARZ_Charge_Nr\n";
      print "next_ARS_RunIdx = $next_ARS_RunIdx\n";
   }

ende:
   return ( $db_ret );
}

#
#  Produktionslinie aus Tabelle Liniengruppe besorgen
#  (wenn mehrere Linien, dann erste nehmen):
#
#  05.07.2013 Liniennummer in Parameter [3] von WinBack
#  REMEMBER ME

sub  get_Linie_von_Liniengruppe
{
   my ( $lg ) = @_;
   my $db_ret = 0;

   my $sql = qq { select LG_Linien from LinienGruppen where LG_Nr = $lg };
   my $sth = $dbh->prepare ( $sql );
   my $anz_rows = $sth->execute();
   if ( $anz_rows == 0 )
   {  $db_ret = -1;
      goto  ende;
   }

   my @row = $sth->fetchrow_array;
   # print "get_Linie_von_Liniengruppe(): LG_Linien = $row[0]\n";
   my @linien = split ( /[,\0]/, $row[0] );
   # for ( my $i = 0; $i < @linien; $i++ )
   # {  print "Linie $linien[$i]\n";
   # }
      # ---- Erste Linie nehmen : ----
   $prod_linie = $linien[0];
      # ---- Daraus 'Beh_Nr': ----
   $Beh_Nr = 100 + $prod_linie;

ende:
   $sth->finish();

   return ( $db_ret );
}


#
#  'next_ARZ_Index' besorgen:
#

sub  get_next_ARZ_Index
{
   my ( $my_Beh_Nr ) = @_;
   my $db_ret = 0;

   $next_ARZ_Index = 0;

   my $sql = qq { select max(ARZ_Index) from ArbRezepte
                  where ARZ_LiBeh_Nr = $my_Beh_Nr };
   my $sth = $dbh->prepare ( $sql );
   my $anz_rows = $sth->execute();
   if ( $anz_rows > 0 )
   {  my @row = $sth->fetchrow_array;
      $next_ARZ_Index = $row[0];
      if ( not $next_ARZ_Index )  { $next_ARZ_Index = 0; }
   }
   $sth->finish();

      # ---- Nächsthöheren Wert verwenden: ----
   if ( $next_ARZ_Index <= 0 )
   {  $next_ARZ_Index = 100000 * $prod_linie + 1;
   }
   else
   {  $next_ARZ_Index += 1;
   }
   return ( $db_ret );
}

#
#  'next_ARZ_Charge_Nr' besorgen:
#

sub  get_next_ARZ_Charge_Nr
{
   my ( $my_Beh_Nr ) = @_;
   my $db_ret = 0;

   $next_ARZ_Charge_Nr = 0;
   my $sql = qq { select max(ARZ_Charge_Nr) from ArbRezepte
                  where ARZ_LiBeh_Nr = $my_Beh_Nr };
   my $sth = $dbh->prepare ( $sql );
   my $anz_rows = $sth->execute();
   if ( $anz_rows > 0 )
   {  my @row = $sth->fetchrow_array;
      $next_ARZ_Charge_Nr = $row[0];
      if ( not $next_ARZ_Charge_Nr )  { $next_ARZ_Charge_Nr = 0; }
   }
   $sth->finish();

      # ---- Nächsthöheren Wert verwenden: ----
   if ( $next_ARZ_Charge_Nr <= 0 )
   {  $next_ARZ_Charge_Nr = 1000 * $prod_linie + 1;
   }
   else
   {  $next_ARZ_Charge_Nr += 1;
   }
   return ( $db_ret );
}


#
#  'next_ARS_RunIdx' besorgen:
#

sub  get_next_ARS_RunIdx
{
   my ( $my_Beh_Nr ) = @_;
   my $db_ret = 0;

   $next_ARS_RunIdx = 0;

   my $sql = qq { select max(ARS_RunIdx) from ArbRZSchritte
                  where ARS_Beh_Nr = $my_Beh_Nr };
   my $sth = $dbh->prepare ( $sql );
   my $anz_rows = $sth->execute();
   if ( $anz_rows > 0 )
   {  my @row = $sth->fetchrow_array;
      $next_ARS_RunIdx = $row[0];
      if ( not $next_ARS_RunIdx )  { $next_ARS_RunIdx = 0; }
   }
   $sth->finish();

      # ---- Nächsthöheren Wert verwenden: ----
   if ( $next_ARS_RunIdx <= 0 )
   {  $next_ARS_RunIdx = 1;
   }
   else
   {  $next_ARS_RunIdx += 1;
   }
   return ( $db_ret );
}


#
#  'RunIdx' auf nächsten 1000er-Bereich erhöhen:
#

sub  start_next_ARS_RunIdx_Bereich
{
   my ( $run_idx ) = @_;
   my $real_wert;
   my $run_idx_neu;

   $real_wert = ($run_idx + 1000) / 1000.0;
   $run_idx_neu = int ( $real_wert ) * 1000;

   return ( $run_idx_neu );
}


#
#  Nächsten 'Art_Index' besorgen:
#

sub  get_next_Art_Index
{
   my ( $my_Beh_Nr ) = @_;
   my $db_ret = 0;

   $next_ARZ_Art_Index = 0;

   my $sql = qq { select max(ARZ_Art_Index) from ArbRezepte
                  where ARZ_LiBeh_Nr = $my_Beh_Nr };
   my $sth = $dbh->prepare ( $sql );
   my $anz_rows = $sth->execute();
   if ( $anz_rows > 0 )
   {  my @row = $sth->fetchrow_array;
      $next_ARZ_Art_Index = $row[0];
      if ( not $next_ARZ_Art_Index )  { $next_ARZ_Art_Index = 0; }
   }
   $sth->finish();

      # ---- Nächsthöheren Wert verwenden: ----
   if ( $next_ARZ_Art_Index == 0 )
   {  $next_ARZ_Art_Index = 100000 * $prod_linie + 1;
   }
   else
   {  $next_ARZ_Art_Index += 1;
   }
   return ( $db_ret );
}


sub  Rezeptmengen_fuer_Charge_umrechnen
{
   my ( $sollmenge_charge ) = @_;
   my $db_ret = 0;

   if ( $log_level >= 1 )
   {  print "Rezeptmengen_fuer_Charge_umrechnen() ...\n";
   }

      # ---- Mengenvariablen zurücksetzen: ----
   $rezept_menge       = 0.0;
   $mengen_faktor_real = 0.0;
   $menge_charge       = 0.0;

      # ---- Rezeptmenge neu berechnen: ----
   Rezeptmenge_berechnen__rs_mengen_umrechnen(1);

   if ( $rezept_menge < 0.001 )
   {  print "*** Rezeptmengen_fuer_Charge_umrechnen(): Rezeptmenge = 0 ! ***\n";
      $db_ret = -1;
      goto  ende;
   }

      # ---- Umrechnungsfaktor für die Mengen in den Rezeptschritten: ----
   $mengen_faktor_real = $sollmenge_charge / $rezept_menge;

   print "RZ_Liniengruppe/RZ_Nr_AlNum/RZ_Bezeichnung:  ";
   print "$rz_liniengruppe/$Rezept_Nr_AlNum/$Rezept_Bez:\n";

   if ( $log_level >= 2 )
   {  printf ( "   Rezeptmenge = %.3f\n", $rezept_menge );
   }

      # ---- Rezeptmengen umrechnen: ----
   Rezeptmenge_berechnen__rs_mengen_umrechnen(2);

   if ( $log_level >= 2 )
   {  printf ( "   Chargenmenge = %.3f (Kontrollwert)\n", $menge_charge );
   }

      # ---- Stückzahl der Charge: ----
   if ( $art_stueck_gewicht < 0.001 )
   {  print "*** Rezeptmengen_fuer_Charge_umrechnen(): Stückgewicht = 0 ! ***\n";
      $stueck_charge = 1;
      #$db_ret = -1;
      #goto  ende;
   }
   else
   {  $stueck_charge = $menge_charge / $art_stueck_gewicht;
   }

      # ---- Prozent Charge bezogen auf Optimalcharge: ----
   if ( $art_charge_opt < 1.0 )
   {  print "*** Rezeptmengen_fuer_Charge_umrechnen(): Optimalcharge = 0 ! ***\n";
      $prozent_charge = 100.0;
      #$db_ret = -1;
      #goto  ende;
   }  
   else
   {  $prozent_charge = 100.0 * $stueck_charge / $art_charge_opt;
   }

ende:
   return ( $db_ret );
}


sub  to_float
{
   my ( $in ) = @_;
   # print ( "   in = $in\n" );
   $in =~ s/,/./;
   # print ( "   out = $in\n" );

   return ( $in );
}

sub float_to_string
{
   my ( $wert, $anz_nk ) = @_;

   my $fmt_str = sprintf ( "%%.%df", $anz_nk );
   my $wert_neu_str = sprintf ( $fmt_str, $wert );

   $wert_neu_str =~ s/[.]/,/;   # !!!!!

   return ( $wert_neu_str );
}


sub  Rezeptmenge_berechnen__rs_mengen_umrechnen
{
   my ( $pass_nr ) = @_;
   my ( $idx, $zeile );
   my ( $kt_nr, $par_nr, $rs_menge, $rs_menge_neu );
   
   for ( $zeile = 0; $zeile < $rz_anz_rows; $zeile++ )
   {     # ---- KT-Nr dieses Satzes: ----
      $idx = $zeile * $rz_num_fields + $idx__KT_Typ_Nr;
      $kt_nr = $rz_rows[$idx];
         # ---- Param-Nr dieses Satzes: ----
      $idx = $zeile * $rz_num_fields + $idx__RS_ParamNr;
      $par_nr = $rz_rows[$idx];

	  # Debug-Ausgabe 
#	  if ( $log_level >= 2 )
#	  { 
#	  	print "KT '$kt_nr' PAR '$par_nr' SOLL '$rs_menge'\n"; 
#	  }

      if ( ($kt_nr == 101 || $kt_nr == 102 || $kt_nr == 103 || $kt_nr == 104 || $kt_nr == 105) &&
           ($par_nr == 1) )
      {
            # ---- Mengenwert dieses Satzes: ----
         $idx = $zeile * $rz_num_fields + $idx__RS_Wert;
         $rs_menge = to_float ( $rz_rows[$idx] );

            # ---- Zur Rezeptmenge hinzufügen: ----
         $rezept_menge += $rs_menge;

            # ---- Menge des Schrittes umrechnen: ----
            # (erst beim 2. Durchlauf wirksam)
         $rs_menge_neu = $rs_menge * $mengen_faktor_real;

            # ---- Umgerechnete Menge in ARS_Wert2 speichern: ----
         $idx = $zeile * $ars_num_fields + $idx__ARS_Wert2;
         $ars_rows[$idx] = $rs_menge_neu;

            # Umrechungsausgabe nur beim 2. Durchlauf:
         if (( $pass_nr == 2 ) and ( $log_level >= 2 ))
         {  printf ( " Menge Rezept/Charge: %.3f/%.3f\n", $rs_menge, $rs_menge_neu );
         }

            # ---- Umgerechnete Menge zur Chargenmenge hinzufügen (Kontrolle): ----
         $menge_charge += $rs_menge_neu;
      }

#	  # Teigzettel drucken - Sollwert bleibt unverändert
#      if ( ($kt_nr == 119) && ($par_nr == 1) )
#     {
#         $rs_menge_neu = $rs_menge;
#         
#         print "RS_MENGE_NEU '$rs_menge_neu'\n";
#         # ---- Umgerechnete Menge in ARS_Wert2 speichern: ----
#         $idx = $zeile * $ars_num_fields + $idx__ARS_Wert2;
#         $ars_rows[$idx] = $rs_menge_neu;
#      }
   }
}

sub  prod_get_seg_index
{
   my ( $linie ) = @_;
   my $db_ret = 0;

   my $sql = qq { select L_Seg_Idx from Linien where L_Nr = $linie };
   my $sth = $dbh->prepare ( $sql );
   my $anz_rows = $sth->execute();
   $seg_idx = -1;
   if ( $anz_rows > 0 )
   {  my @row = $sth->fetchrow_array;
      $seg_idx = $row[0];
   }
   $sth->finish();

   return ( $db_ret );
}


sub  Charge_und_Schritte_speichern
{
   my $db_ret = 0;

   # (@V1.5) Bestellnummer eingefuegt
   ( $bestell_nr ) = @_;
   
   $db_ret = Charge_in_ArbRezepte_speichern();
   if ( $db_ret < 0 )  { goto  ende; }

   $db_ret = Schritte_in_ArbRZSchritte_speichern();
   if ( $db_ret < 0 )  { goto  ende; }

ende:
   return ( $db_ret );
}


sub  Charge_in_ArbRezepte_speichern
{
   my $db_ret = 0;
   my $arz_ins_felder;
   my $arz_ins_values;

   if ( $log_level >= 1 )
   {  print "Charge_in_ArbRezepte_speichern() ...\n";
   }

      # ---- Floats in String umwandeln und Dezimal-Separator ändern: ----
   my $menge_charge_str   = float_to_string ( $menge_charge, 1 );
   my $stueck_charge_str  = float_to_string ( $stueck_charge, 0 );
   my $prozent_charge_str = float_to_string ( $prozent_charge, 3 );

if ( $DB_NEU > 1 )
{
      # --------- 02.10.2007: Feld eingefuegt ARZ_Best_Nr: ----------
      # --------- 18.04.2006: Aktuelle Insert-Felder ArbRezepte: ----------
   $arz_ins_felder = "ARZ_TW_Nr, ARZ_LiBeh_Nr, ARZ_Index,
           ARZ_KA_NrAlNum, ARZ_Art_Index, ARZ_Charge_Nr, ARZ_Best_Nr, ARZ_Erststart,
           ARZ_Nr, ARZ_RZ_Typ, ARZ_Bezeichnung, ARZ_Typ, ARZ_Art_Einheit,
           ARZ_RZ_Nr_AlNum, ARZ_RZ_Bezeichnung,
           ARZ_Sollmenge_kg, ARZ_Sollmenge_stueck, ARZ_Anstellgut_kg,
           ARZ_Seg_Nr, ARZ_Tag_offs, ARZ_geloescht, ARZ_RZ_Gewicht";
   $arz_ins_values = "?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?";
}
else
{
      # ---------- Alt: ----------
   $arz_ins_felder = "ARZ_TW_Nr, ARZ_LiBeh_Nr, ARZ_Index,
           ARZ_KA_NrAlNum, ARZ_Art_Index, ARZ_Charge_Nr, ARZ_Zp_Gestartet,
           ARZ_Nr, ARZ_RZ_Typ, ARZ_Bezeichnung, ARZ_Typ, ARZ_Art_Einheit,
           ARZ_RZ_Nr_AlNum, ARZ_RZ_Bezeichnung,
           ARZ_Sollmenge_kg, ARZ_Sollmenge_stueck, ARZ_Anstellgut_kg,
           ARZ_Seg_Nr, ARZ_Tag_offs, ARZ_geloescht";
   $arz_ins_values = "?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?";
}

   my $sql_arz_insert = qq
   {  insert into ArbRezepte ( $arz_ins_felder ) values ( $arz_ins_values )
   };
   my $sth_arz_insert = $dbh->prepare ( $sql_arz_insert );

   if ( $log_level >= 5 )
   {  print "**** Charge abspeichern:\n";
      print "   ARZ_LiBeh_Nr = $Beh_Nr\n";
      print "   ARZ_Index = $next_ARZ_Index\n";
   }

   # goto  finish;

if ( $DB_NEU > 1 )
{
   $sth_arz_insert->execute
   (
      0,                    # ARZ_TW_Nr             Bei neuen Sätzen immer 0
      $Beh_Nr,              # ARZ_LiBeh_Nr          Linien-Nr + 100
      $next_ARZ_Index,
      $art_nr_alnum,        # ARZ_KA_NrAlNum        Alphanumerische Artikel-Nr
      $next_ARZ_Art_Index,  # ARZ_Art_Index
      $next_ARZ_Charge_Nr,  # ARZ_Charge_Nr         Charge-Nr
      $bestell_nr,	   		# ARZ_Best_Nr	    	Bestellnummer (@V1.5)
      local_Now(),          # ARZ_Erststart         Zeitpunkt des Anlegens des Satzes
      $art_rz_nr,           # ARZ_Nr                interne Rezept-Nr.
      $rz_variante_nr,      # ARZ_RZ_Typ            Rezept-Variante-Nr.
      $Artikel_Bez,         # ARZ_Bezeichnung       Bezeichnung Artikel
      $Artikel_Typ,         # ARZ_Typ               Unterscheidung Artikel/Rezept
      $Artikel_Einheit,     # ARZ_Art_Einheit       Artikel: Stück oder kg, Rezept: kg
      $Rezept_Nr_AlNum,     # ARZ_RZ_Nr_AlNum       Alphanumerische Rezept-Nr
      $Rezept_Bez,          # ARZ_RZ_Bezeichnung    Bezeichnung Rezept
      $menge_charge_str,    # ARZ_Sollmenge_kg      Menge Charge
      $stueck_charge_str,   # ARZ_Sollmenge_stueck  Stückzahl Charge
      $prozent_charge_str,  # ARZ_Anstellgut_kg     Prozent Charge
      $seg_idx,             # ARZ_Seg_idx           Produktionssegment-Index
      0,                    # ARZ_Tag_offs          Tag_offs = 0: heute
      0,                    # ARZ_geloescht         geloescht = 0: nicht gelöscht
      $Rezept_Gewicht       # ARZ_RZ_Gewicht
   );
}
else
{
   $sth_arz_insert->execute
   (
      0,                 # ARZ_TW_Nr     Bei neuen Sätzen immer 0
      $Beh_Nr,           # ARZ_LiBeh_Nr  Linien-Nr + 100
      $next_ARZ_Index,
      $art_nr_alnum,        # ARZ_KA_NrAlNum     Alphanumerische Artikel-Nr
      $next_ARZ_Art_Index,  # ARZ_Art_Index
      $next_ARZ_Charge_Nr,  # ARZ_Charge_Nr      Charge-Nr
      local_Now(),          # ARZ_Erststart  Zeitpunkt des Anlegens des Satzes
      $art_rz_nr,        # ARZ_Nr            interne Rezept-Nr.
      $rz_variante_nr,   # ARZ_RZ_Typ        Rezept-Variante-Nr.
      $Artikel_Bez,      # ARZ_Bezeichnung   Bezeichnung Artikel
      $Artikel_Typ,      # ARZ_Typ           Unterscheidung Artikel/Rezept
      $Artikel_Einheit,  # ARZ_Art_Einheit   Artikel: Stück oder kg, Rezept: kg
      $Rezept_Nr_AlNum,  # ARZ_RZ_Nr_AlNum       Alphanumerische Rezept-Nr
      $Rezept_Bez,       # ARZ_RZ_Bezeichnung    Bezeichnung Rezept
      $menge_charge_str, # ARZ_Sollmenge_kg      Menge Charge
      $stueck_charge_str,# ARZ_Sollmenge_stueck  Stückzahl Charge
      $prozent_charge_str, # ARZ_Anstellgut_kg     Prozent Charge
      $seg_idx,          # ARZ_Seg_idx        Produktionssegment-Index
      0,                 # ARZ_Tag_offs    Tag_offs = 0: heute
      0                  # ARZ_geloescht   geloescht = 0: nicht gelöscht
   );
}

finish:

   $sth_arz_insert->finish();

   return ( $db_ret );
}


sub  local_Now
{
   my ( $sec, $min, $std, $tag, $mon, $jahr,
        $wtag, $jahrestag, $sommerzeit ) = localtime ( time );
   my $time_str;

   $jahr += 1900;
   $mon += 1;

   $time_str = sprintf ( "%d-%02d-%02d %02d:%02d:%02d", $jahr,$mon,$tag,$std,$min,$sec );

   return ( $time_str );
}


sub  Schritte_in_ArbRZSchritte_speichern
{
   my $db_ret = 0;
   my $ars_ins_felder;
   my $ars_ins_values;

   if ( $log_level >= 1 )
   {  print "Schritte_in_ArbRezepte_speichern() ...\n";
   }

if ( $DB_NEU > 1 )
{
   $ars_ins_felder = "ARS_TW_Nr, ARS_ARZ_Index, ARS_Beh_Nr, ARS_RunIdx,
                      ARS_Art_Index, ARS_Charge_Nr, ARS_RZ_Nr,
           ARS_Index, ARS_Schritt_Nr, ARS_Ko_Nr, ARS_ParamNr, ARS_Wert,
           ARS_RS_Par1, ARS_RS_Par2, ARS_RS_Par3,
           ARS_KO_Nr_AlNum, ARS_KO_Bezeichnung, ARS_KO_Temp_Korr,
           ARS_KP_Wert,
           ARS_KT_Typ_Nr, ARS_KT_Rezept, ARS_KT_Bezeichnung,
           ARS_KT_KurzBez, ARS_KT_EinheitIndex,
           ARS_KT_Format, ARS_KT_Laenge, ARS_KT_UnterGW, ARS_KT_OberGW,
           ARS_E_Einheit,
           ARS_RS_Wert";
   $ars_ins_values = "?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?";

   #     // 23.11.2015: Feld ARS_ARZ_Index eingebaut
   #
   #     // 19.04.2006: Fehlende Felder für Flat-Produktion eingebaut:
   #  ARS_KO_Nr_AlNum, ARS_KO_Bezeichnung, ARS_KO_Temp_Korr,
   #  ARS_KP_Wert,
   #  ARS_KT_Typ_Nr, ARS_KT_Rezept, ARS_KT_Bezeichnung,
   #  ARS_KT_KurzBez, ARS_KT_EinheitIndex,
   #  ARS_KT_Format, ARS_KT_Laenge, ARS_KT_UnterGW, ARS_KT_OberGW,
   #  ARS_E_Einheit,
   #  ARS_RS_Wert
}
else
{
   $ars_ins_felder = "ARS_TW_Nr, ARS_Beh_Nr, ARS_RunIdx,
                      ARS_Art_Index, ARS_Charge_Nr, ARS_RZ_Nr,
           ARS_Index, ARS_Schritt_Nr, ARS_Ko_Nr, ARS_ParamNr, ARS_Wert,
           ARS_RS_Par1, ARS_RS_Par2, ARS_RS_Par3";
   $ars_ins_values = "?,?,?,?,?,?,?,?,?,?,?,?,?,?";
}

   my $sql_ars_insert = qq
   {  insert into ArbRZSchritte ( $ars_ins_felder ) values ( $ars_ins_values )
   };
   my $sth_ars_insert = $dbh->prepare ( $sql_ars_insert );

   # ---- Abspeichern direkt aus Rezept-Array: ----

   my ( $zeile, $spalte, $idx );
   my ( $ars_index, $ars_schritt_nr, $rs_kt_nr, $ars_ko_nr, $ars_paramnr,
        $ars_wert, $ars_rs_par1, $ars_rs_par2, $ars_rs_par3,
        $ars_ko_nr_alnum, $ars_ko_bezeichnung, $ars_ko_temp_korr,
        $ars_kp_wert,
        $ars_kt_typ_nr, $ars_kt_rezept, $ars_kt_bezeichnung,
        $ars_kt_kurzbez, $ars_kt_einheitindex,
        $ars_kt_format, $ars_kt_laenge, $ars_kt_untergw, $ars_kt_obergw,
        $ars_e_einheit,
        $ars_rs_wert );

	# Initialisierung
	$ars_rs_par1 = "";
	$ars_rs_par2 = "";
	$ars_rs_par3 = "";
	
   for ( $zeile = 0; $zeile < $rz_anz_rows; $zeile++ )
   {
         # ---- RS_Index --> ARS_Index: ----
      $idx = $zeile * $rz_num_fields + $idx__RS_Index;
      $ars_index = $rz_rows[$idx];
         # ---- RS_Schritt_Nr --> ARS_Schritt_Nr: ----
      $idx = $zeile * $rz_num_fields + $idx__RS_Schritt_Nr;
      $ars_schritt_nr = $rz_rows[$idx];
         # ---- RS_Ko_Nr --> ARS_Ko_Nr: ----
      $idx = $zeile * $rz_num_fields + $idx__RS_Ko_Nr;
      $ars_ko_nr = $rz_rows[$idx];
         # ---- RS_ParamNr --> ARS_ParamNr: ----
      $idx = $zeile * $rz_num_fields + $idx__RS_ParamNr;
      $ars_paramnr = $rz_rows[$idx];
         # ---- KT-Nr besorgen (um Textkomponente erkennen zu können): ----
      $idx = $zeile * $rz_num_fields + $idx__KT_Typ_Nr;
      $rs_kt_nr = $rz_rows[$idx];

         # ---- Bei 'ars_paramnr' = 1 ARS_Wert2 --> ARS_Wert,
# 17.12.2004: Nur bei Parameter-Nr. 1 den umgerechneten Mengenwert nehmen.
#             Bei den anderen Parametern RS_Wert --> ARS_Wert.
#             Ausnahme: Der Text der Textkomponente KT121 ist auch in Par.1
# 24.10.2008: Erweiterung Kneterschritte
# 24.10.2008: Erweiterung Produktions-Stufen/Kessel
# 14.02.2013: Erweiterung Teigzettel
      if ( $ars_paramnr == 1 && $rs_kt_nr != 121 && $rs_kt_nr != 122 && $rs_kt_nr != 123 && $rs_kt_nr != 118 && $rs_kt_nr != 128 && $rs_kt_nr != 111 && $rs_kt_nr != 119 )
      {  $idx = $zeile * $ars_num_fields + $idx__ARS_Wert2;
         $ars_wert = $ars_rows[$idx];
      }
      else
      {  $idx = $zeile * $rz_num_fields + $idx__RS_Wert;
    	 $ars_wert = $rz_rows[$idx];
      }

      if ( $DB_NEU > 0 )
      {
            # ---- RS_Par1 --> ARS_RS_Par1: ----
         $idx = $zeile * $rz_num_fields + $idx__RS_Par1;
         $ars_rs_par1 = $rz_rows[$idx];
            # ---- RS_Par2 --> ARS_RS_Par2: ----
         $idx = $zeile * $rz_num_fields + $idx__RS_Par2;
         $ars_rs_par2 = $rz_rows[$idx];
            # ---- RS_Par3 --> ARS_RS_Par3: ----
         $idx = $zeile * $rz_num_fields + $idx__RS_Par3;
         $ars_rs_par3 = $rz_rows[$idx];
      }

      if ( $DB_NEU > 1 )
      {
            # ---- KO_Nr_AlNum --> ARS_KO_Nr_AlNum: ----
         $idx = $zeile * $rz_num_fields + $idx__KO_Nr_AlNum;
         $ars_ko_nr_alnum = $rz_rows[$idx];
            # ---- KO_Bezeichnung --> ARS_KO_Bezeichnung: ----
         $idx = $zeile * $rz_num_fields + $idx__KO_Bezeichnung;
         $ars_ko_bezeichnung = $rz_rows[$idx];
            # ---- KO_Temp_Korr --> ARS_KO_Temp_Korr: ----
         $idx = $zeile * $rz_num_fields + $idx__KO_Temp_Korr;
         $ars_ko_temp_korr = $rz_rows[$idx];
            # ---- KP_Wert --> ARS_KP_Wert: ----
         $idx = $zeile * $rz_num_fields + $idx__KP_Wert;
         $ars_kp_wert = $rz_rows[$idx];
            # ---- KT_Typ_Nr --> ARS_KT_Typ_Nr: ----
         $idx = $zeile * $rz_num_fields + $idx__KT_Typ_Nr;
         $ars_kt_typ_nr = $rz_rows[$idx];
            # ---- KT_Rezept --> ARS_KT_Rezept: ----
         $idx = $zeile * $rz_num_fields + $idx__KT_Rezept;
         $ars_kt_rezept = $rz_rows[$idx];
            # ---- KT_Bezeichnung  --> ARS_KT_Bezeichnung: ----
         $idx = $zeile * $rz_num_fields + $idx__KT_Bezeichnung;
         $ars_kt_bezeichnung = $rz_rows[$idx];
            # ---- KT_KurzBez --> ARS_KT_KurzBez: ----
         $idx = $zeile * $rz_num_fields + $idx__KT_KurzBez;
         $ars_kt_kurzbez = $rz_rows[$idx];
            # ---- KT_EinheitIndex --> ARS_KT_EinheitIndex: ----
         $idx = $zeile * $rz_num_fields + $idx__KT_EinheitIndex;
         $ars_kt_einheitindex = $rz_rows[$idx];
            # ---- KT_Format --> ARS_KT_Format: ----
         $idx = $zeile * $rz_num_fields + $idx__KT_Format;
         $ars_kt_format = $rz_rows[$idx];
            # ---- KT_Laenge --> ARS_KT_Laenge: ----
         $idx = $zeile * $rz_num_fields + $idx__KT_Laenge;
         $ars_kt_laenge = $rz_rows[$idx];
            # ---- KT_UnterGW --> ARS_KT_UnterGW: ----
         $idx = $zeile * $rz_num_fields + $idx__KT_UnterGW;
         $ars_kt_untergw = $rz_rows[$idx];
            # ---- KT_OberGW --> ARS_KT_OberGW: ----
         $idx = $zeile * $rz_num_fields + $idx__KT_OberGW;
         $ars_kt_obergw = $rz_rows[$idx];
            # ---- E_Einheit --> ARS_E_Einheit: ----
         $idx = $zeile * $rz_num_fields + $idx__E_Einheit;
         $ars_e_einheit = $rz_rows[$idx];
            # ---- RS_Wert --> ARS_RS_Wert: ----
         $idx = $zeile * $rz_num_fields + $idx__RS_Wert;
         $ars_rs_wert = $rz_rows[$idx];
      }

      if ( $log_level >= 5 )
      {  print "**** Schritt abspeichern:\n";
         print "   ARS_Beh_Nr = $Beh_Nr\n";
         print "   ARS_RunIdx = $next_ARS_RunIdx\n";
         if ( $DB_NEU > 0 )
         {  print "   ARS_Art_Index = $next_ARZ_Art_Index\n";
         }
         print "   ARS_Charge_Nr = $next_ARZ_Charge_Nr\n";
         print "   ARS_RZ_Nr = $art_rz_nr\n";
         print "   ARS_Index = $ars_index\n";
         print "   ARS_Schritt_Nr = $ars_schritt_nr\n";
         print "   ARS_Ko_Nr = $ars_ko_nr\n";
         print "   ARS_ParamNr = $ars_paramnr\n";
         print "   ARS_Wert = $ars_wert\n";
#         if ( $DB_NEU > 0 )
#         {  print "   ARS_RS_Par1 = $ars_rs_par1\n";
#            print "   ARS_RS_Par2 = $ars_rs_par2\n";
#            print "   ARS_RS_Par3 = $ars_rs_par3\n";
#         }
#         if ( $DB_NEU > 1 )
#         {
#         }
      }

      # goto  next_schritt;

         # ---- Floats in String umwandeln und Dezimal-Separator ändern: ----
# print "****** vor float_to_string(): ars_wert = $ars_wert *******\n";
# 19.12.2004: Nur bei Parameter-Nr. 1 in String umwandeln,
#             sonst direkt übernehmen (z.B. bei Wassertemperatur):
#             Ausnahme auch hier: Nicht umwandeln bei Textkomponente KT121
# 24.10.2008: Erweiterung Kneterschritte
# 24.10.2008: Erweiterung Produktions-Stufen/Kessel
# 14.02.2013: Erweiterung Teigzettel
      my $ars_wert_str;
      if ( $ars_paramnr == 1 && $rs_kt_nr != 121 && $rs_kt_nr != 122 && $rs_kt_nr != 123 && $rs_kt_nr != 118 && $rs_kt_nr != 128 && $rs_kt_nr != 111 && $rs_kt_nr != 119 )
      {  $ars_wert_str = float_to_string ( $ars_wert, 5 );
      }
      else
      {  $ars_wert_str = $ars_wert;
      }
# print "****** nach float_to_string(): ars_wert_str = $ars_wert_str *******\n";

if ( $DB_NEU > 1 )
{
      $sth_ars_insert->execute
      (
         0,                  # ARS_TW_Nr     Bei neuen Sätzen immer 0
         $next_ARZ_Index,	 # ARS_ARZ_Index auf Tabelle ArbRezepte
         $Beh_Nr,            # ARZ_Beh_Nr    Linien-Nr + 100
         $next_ARS_RunIdx,   # ARS_RunIdx
         $next_ARZ_Art_Index,  # ARZ_Art_Index   Art_Index
         $next_ARZ_Charge_Nr,  # ARZ_Charge_Nr   Charge-Nr
         $art_rz_nr,           # ARS_RZ_Nr       interne Rezept-Nr.
            # ---- Rezeptschritt-Felder: ----
         $ars_index,        # ARS_Index
         $ars_schritt_nr,   # ARS_Schritt_Nr
         $ars_ko_nr,        # ARS_Ko_Nr
         $ars_paramnr,      # ARS_ParamNr
         $ars_wert_str,     # ARS_Wert
         $ars_rs_par1,      # ARS_RS_Par1
         $ars_rs_par2,      # ARS_RS_Par2
         $ars_rs_par3,      # ARS_RS_Par3
            # ---- 19.04.2006: Felder für Flat-Produktion: ----
         $ars_ko_nr_alnum,     # ARS_KO_Nr_AlNum
         $ars_ko_bezeichnung,  # ARS_KO_Bezeichnung
         $ars_ko_temp_korr,    # ARS_KO_Temp_Korr
         $ars_kp_wert,         # ARS_KP_Wert
         $ars_kt_typ_nr,       # ARS_KT_Typ_Nr
         $ars_kt_rezept,       # ARS_KT_Rezept
         $ars_kt_bezeichnung,  # ARS_KT_Bezeichnung
         $ars_kt_kurzbez,      # ARS_KT_KurzBez
         $ars_kt_einheitindex, # ARS_KT_EinheitIndex
         $ars_kt_format,       # ARS_KT_Format
         $ars_kt_laenge,       # ARS_KT_Laenge
         $ars_kt_untergw,      # ARS_KT_UnterGW
         $ars_kt_obergw,       # ARS_KT_OberGW
         $ars_e_einheit,       # ARS_E_Einheit
         $ars_rs_wert          # ARS_RS_Wert
      );
}
else
{
      $sth_ars_insert->execute
      (
         0,                  # ARS_TW_Nr     Bei neuen Sätzen immer 0
         $Beh_Nr,            # ARZ_Beh_Nr    Linien-Nr + 100
         $next_ARS_RunIdx,   # ARS_RunIdx
         $next_ARZ_Art_Index,  # ARZ_Art_Index   Art_Index
         $next_ARZ_Charge_Nr,  # ARZ_Charge_Nr   Charge-Nr
         $art_rz_nr,           # ARS_RZ_Nr       interne Rezept-Nr.
            # ---- Rezeptschritt-Felder: ----
         $ars_index,        # ARS_Index
         $ars_schritt_nr,   # ARS_Schritt_Nr
         $ars_ko_nr,        # ARS_Ko_Nr
         $ars_paramnr,      # ARS_ParamNr
         $ars_wert_str,     # ARS_Wert
         $ars_rs_par1,      # ARS_RS_Par1
         $ars_rs_par2,      # ARS_RS_Par2
         $ars_rs_par3       # ARS_RS_Par3
      );
}

next_schritt:
      $next_ARS_RunIdx += 1;
   }

finish:
   $sth_ars_insert->finish();

   return ( $db_ret );
}

# ---------------------------------------------------------------------------
1;
# ---------------------------------------------------------------------------

