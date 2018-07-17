#!/usr/bin/perl -w

#
# ------ Import Produktionsdaten, Version 3.3 --------
#
# Aenderung gegenueber Version 3.2:
# Feld Produktionsblock (entspricht Artikel.ProduktionsLinie) neu eingefuegt
#
# Aenderung gegenueber Version 3.1:
# Feld ARS_ARZ_Index neu hinzugef�gt in Tabelle ArbRZSchritte
# ArbRZSchritte.ARS_ARZ_Index = ArbRezepte.ARZ_Index
#
# Aenderung gegenueber Version 3.0:
#
# Parameter 3 entf�llt (Liniennummer von WinBack)
#
# Aenderung gegenueber Version 2.4:
#
# Einlesen der Daten spezifisch f�r eine Produktions-Linie
# Die Linien-Nummer wird von WinBack �bergeben
#
# Aenderung gegenueber Version 2.3:
#
# Import Produktionsdaten auch Rohstoffe als Artikel-
# zeile (Type 102)
#
# Aenderung gegenueber Version 2.2:
#
# Komponente Teigzetteldruck wird mit Sollwert
# importiert
#
# Aenderung gegenueber Version 2.1:
#
# Import auch wenn Chargengrsse = 0
# z.B. nach Import der Daten von Optimo
#
# Aenderung gegenueber Version 2.0:
#
# Fehler behoben beim Import/Umrechnen von Kneterzeilen
# (H�ussler Memmingen)
#
# Aenderung gegenueber Version 1.9:
#
# Import von Rezepten (Artikel='K') ge�ndert.
# SQL-Anweisung korrigiert
# Dateinamen ge�ndert (ohne Versions-Nummer)
#
# Aenderung gegenueber Version 1.8:
#
# sql-Statements ge�ndert: Feldvariablen f�r Artikel- und
# Rezeptnummern mit Hochkomma eingetragen
#
# Aenderung gegenueber Version 1.7:
# 
# Auswertung der �bergebenen Parameter auch
# wenn nur 1 Parameter �bergeben wird
#
#
# Aenderung gegenueber Version 1.6:
#
# Modul prodimport-sub.pm
# automatische Umschaltung zwischen Artikelzeilen und Rezeptzeilen
# Rezeptzeilen enthalten im Artikelfeld die Kennzeichnung 'K' fuer
# Kopfzeile (zusammengefasste Teige)	
#
#
# Aenderung gegenueber Version 1.5:
# 
# Modul prodimp.pl:
#    Datenbank + IP-Adresse als Kommandozeilenargument
# Modul prodimport-sub.pm
#	 Import von Datens�tzen ohne Artikelnummer (nur Rezeptnummern)
#
# Aenderung gegenueber Version 1.4:
#
# Modul prodimp.pl:
#    Feld ARZ_Best_Nr hinzugefuegt
#
#
# Aenderung gegenueber Version 1.2:
#
# Modul prodimp.pl:
#    Funktion 'Artikel_Laden()': In SQL-Statement $sql_artikel_laden
#    Variable $art_nr_alnum_ mit Hochkommas versehen, damit MySql
#    mit KO_Nr_AlNum = $art_nr_alnum_ keine Integer-Vergleiche
#    durchfuehrt (Idee von Joerg).
#
#
#
#  Artikelstamm (Komponenten und Artikel) importiern
#

#todo Import-Pfad prodimport-sub in @INC
#
#
use strict;
use DBI;

require ( "prodimport-sub.pm" );
my $version = "3.3";

print "\n";
print "************* Produktionsdaten-Import, Version $version **************\n";

my $ko_file;
my $ko_base;
my $ko_linie;

my $num_args = $#ARGV + 1;
if ( $num_args == 0 )
{
  	$ko_file  = "bakelink/1101_Produktion.csv";
	$ko_base  = "winback:localhost";
	$ko_linie = 0;
	
   	print "Keine Import-Datei angegeben. Nehme '$ko_file' ...\n";
   	print "Keine Produktions-Linie angegeben. Nehme '$ko_linie' ...\n";
}
else
{
	if ( $num_args == 2 )
	{
		$ko_file = $ARGV[0];
		$ko_base = $ARGV[1];
	}
	else
	{
		$ko_file = $ARGV[0];
		$ko_base  = "winback:localhost";
		$ko_linie = 0;
	}
}
my $log_level = 2;

   # **** Globale Variablen: ****
my $line;
my $zeilen = 0;     # Laufende Zeilen-Nr. der Import-Datei
my @feld_text;      # Array der Beschreibungen zu den Tokens
my @feld_name;      # Array der gelieferten Datenbank-Felder
my $anz_feld_namen; # Anzahl der vorgefundenen Feldnamen
my @feld_wert;      # Array der gelieferten Werte

   # **** Unterprogramme: ****
sub  tokenize_zeile;
sub  get_anzahl_tokens;
sub  get_anzahl_tokens_mit_inhalt;
sub  werte_zeile_importieren;

#
# ***** Verarbeiten der Importdatei mit Produktionsdaten: *****
#

print "**** Start Import ...\n";

print "Import File '$ko_file'\n";
print "IP-Adresse  '$ko_base'\n";

open ( FILE1, $ko_file ) || die "Fehler open(): '$ko_file'";

db_connect ( $ko_base, 'herbst', 'herbst' );

# while ( ($line = <FILE1>) && ($zeilen < 3) )
while ( ($line = <FILE1>) )
{
   my $anz;
   if ( $zeilen == 0 )
   {
         # ----- Zeile 0: Feldtexte �bernehmen: -----
      @feld_text = tokenize_zeile;
      $anz = get_anzahl_tokens ( @feld_text );
      print "Zeile $zeilen (Anzahl der Feldtexte): $anz Tokens\n";
   }
   else
   {  if ( $zeilen == 1 )
      {
            # ----- Zeile 1: Feldnamen �bernehmen: -----
         @feld_name = tokenize_zeile;
         $anz = get_anzahl_tokens ( @feld_name );
         print "Zeile $zeilen (Anzahl der Feldnamen): $anz Tokens\n";
            #
         $anz_feld_namen = get_anzahl_tokens_mit_inhalt ( @feld_name );
            #
         print "---> Anzahl VORHANDENE Feldnamen: $anz_feld_namen\n";
      }
      else
      {  @feld_wert = tokenize_zeile;
         get_anzahl_tokens ( @feld_wert );
         # feld_werte_ausgeben;
         werte_zeile_importieren;
      }
   }

   $zeilen += 1;
}

print "**** Ende Import.\n";

print "$zeilen Zeilen aus Datei '$ko_file' gelesen\n";

db_disconnect();

close ( FILE1 );

# REMEMBER ME - TESTVERSION
unlink ( $ko_file );

exit;

# ------------------------------------------------------------------

# ************************ Unterprogramme: *************************


# ----- Zeile in Tokens zerlegen: -----

sub  tokenize_zeile
{
   split ( /[,\x0D\x0A]/, $line );
}

# ----- Anzahl der Tokens feststellen: -----

sub  get_anzahl_tokens
{
   my ( $tok_idx, $result );

   $tok_idx = 0;
   while ( $tok_idx < @_ )
   {  $tok_idx += 1;
   }
   # if ( $log_level >= 1 )
   # {  print "Zeile $zeilen: $tok_idx Tokens\n";
   # }

   $result = $tok_idx;
}


# ----- Anzahl der Tokens mit Inhalt feststellen: -----

sub  get_anzahl_tokens_mit_inhalt
{
   my ( @array ) = @_;
   my ( $tok_idx, $tok_str, $len );
   my ( $anz, $result );

   $anz = 0;
   $tok_idx = 0;
   while ( $tok_idx < @array )
   {
      $tok_str = $array[$tok_idx];
      $len = length $tok_str;
      if ( $len > 0 )
      {  if ( $log_level >= 2 )
         {
            print "---> Feld[$tok_idx]: '$tok_str'\n";
         }
         $anz += 1;
      }
      $tok_idx += 1;
   }

   $result = $anz;
};


# ----- Eine Import-Zeile bearbeiten: -----

sub  werte_zeile_importieren
{
   my ( $tok_idx, $name, $len_name, $wert );

   my ( $anz_imp_felder ) = 0;     # Anzahl der notwendigen Import-Felder
   my ( $art_nr_alnum ) = "";
   my ( $rz_nr_alnum ) = "";
   my ( $rz_variante_nr ) = 1;
   my ( $sollmenge_charge ) = 0.0;
   my ( $bestell_nr ) = "";
   my ( $art_linie_nr ) = "";


   $tok_idx = 0;
   while ( $tok_idx < @feld_name )
   {
      $name = $feld_name[$tok_idx];    # Feldname dieses Tokens
      $len_name = length $name;
         # Feldname vorhanden und Wert-Token zu diesem Namen vorhanden ?:
      if ( ($len_name > 0) && ($tok_idx < @feld_wert) )
      {
         # ----- Feldwert- und Feldname-Token vorhanden -----

         $wert = $feld_wert[$tok_idx];

         if ( $log_level >= 2 )
         {  print "Zeile $zeilen, Feld[$tok_idx] ('$name'): '$wert'\n";
         }

         if ( $name eq 'ARZ_KA_NrAlNum' )    # Artikel-Nr (alphanumerische)
         {  $art_nr_alnum = $wert;
            $anz_imp_felder += 1;
         };
         if ( $name eq 'ARZ_RZ_Typ' )        # Variante-Nr
         {  $rz_variante_nr = $wert;
            $anz_imp_felder += 1;
         };
         if ( $name eq 'ARZ_RZ_Nr_AlNum' )   # Rezept-Nr (alphanumerische)
         {  $rz_nr_alnum = $wert;
            $anz_imp_felder += 1;
         };
         if ( $name eq 'ARZ_Sollmenge_kg' )  # Sollmenge
         {  $sollmenge_charge = $wert;
            $anz_imp_felder += 1;
         };
         if ( $name eq 'ARZ_Best_Nr' )       # Bestellnummer
         {  $bestell_nr = $wert;
            $anz_imp_felder += 1;
         };
         if ( $name eq 'ARZ_LiBeh_Nr' )      # Produktionsblock entspricht Artikel-Linien-Nummer
         {  $art_linie_nr = $wert;
            $anz_imp_felder += 1;
         };
      }
      else
      {  # print "Zeile $zeilen, Feld[$tok_idx]: Kein Feldname und/oder Feldwert vorhanden\n";
      }

      $tok_idx += 1;
   }

   if ( $anz_imp_felder >= 5 )
   {      
      my $db_ret = 0;

      $db_ret = Artikel_und_Rezept_laden ( $art_nr_alnum, $rz_variante_nr, $rz_nr_alnum );
      if ( $db_ret < 0 )   { goto  ende; }

      $db_ret = Indizes_berechnen();
      if ( $db_ret < 0 )   { goto  ende; }

      $db_ret = Rezeptmengen_fuer_Charge_umrechnen ( $sollmenge_charge );
      if ( $db_ret < 0 )   { goto  ende; }

      $db_ret = Charge_und_Schritte_speichern( $bestell_nr , $art_linie_nr );	# (@V1.5) Bestell-Nummer eingefuegt (@V3.2) Artikel-Linien-Nummer eingefuegt
      if ( $db_ret < 0 )   { goto  ende; }

   }
   else
   {
      print "Zeile $zeilen: Anzahl Import-Felder: Soll = 6  Ist = $anz_imp_felder\n";
   }

ende:
   return;
}

# ------------------------------------------------------------------

exit;

