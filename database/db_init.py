import sqlite3
import os

conn = sqlite3.connect("fingerprint.db")

cursor = conn.cursor()

# Create the biodata table
cursor.execute('''
CREATE TABLE IF NOT EXISTS biodata (
  NIK TEXT PRIMARY KEY,
  nama TEXT,
  tempat_lahir TEXT,
  tanggal_lahir DATE,
  jenis_kelamin TEXT CHECK(jenis_kelamin IN ('Laki-Laki', 'Perempuan')),
  golongan_darah TEXT,
  alamat TEXT,
  agama TEXT,
  status_perkawinan TEXT CHECK(status_perkawinan IN ('Belum Menikah', 'Menikah', 'Cerai')),
  pekerjaan TEXT,
  kewarganegaraan TEXT
);
''')

# Create the sidik_jari table
cursor.execute('''
CREATE TABLE IF NOT EXISTS sidik_jari (
  berkas_citra TEXT,
  nama TEXT
);
''')