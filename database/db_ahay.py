import sqlite3
import random

# Function to convert a name to its alay version
def convert_alay(name):
    map_alay = {
        'a': '4', 'A': '4',
        'b': '8', 'B': '8',
        'c': '(', 'C': '(',
        'e': '3', 'E': '3',
        'g': '6', 'G': '6',
        'i': '1', 'I': '1',
        'l': '1', 'L': '1',
        'o': '0', 'O': '0',
        's': '5', 'S': '5',
        't': '7', 'T': '7',
        'z': '2', 'Z': '2'
    }
    
    # Randomly remove some letters (but not spaces)
    result = []
    for char in name:
        if char != ' ' and random.random() < 0.5: # 50 % chance convert to alay
            char = map_alay.get(char,char)
            
        if char != ' ' and random.random() < 0.1:  # 10% chance remove character
            continue
        
        result.append(char)
    
    return ''.join(result)

# Connect to the SQLite database
conn = sqlite3.connect('test.db')
cursor = conn.cursor()

# Create biodata table if it doesn't exist
cursor.execute('''
    CREATE TABLE IF NOT EXISTS biodata (
        PK VARCHAR(16) NOT NULL,
        nama VARCHAR(100),
        tempat_lahir VARCHAR(50),
        jenis_kelamin VARCHAR(6) CHECK (jenis_kelamin IN ('male', 'female')),
        golongan_darah VARCHAR(3) CHECK (golongan_darah IN ('A', 'B', 'AB', 'O')),
        alamat VARCHAR(255),
        agama VARCHAR(100),
        status_perkawinan VARCHAR(13) CHECK (status_perkawinan IN ('Belum Menikah', 'Menikah', 'Cerai')),
        pekerjaan VARCHAR(100),
        kewarganegaraan VARCHAR(50),
        PRIMARY KEY (PK)
    );
''')

# Fetch distinct nama values from sidik_jari table
cursor.execute('SELECT DISTINCT nama FROM sidik_jari')
distinct_names = cursor.fetchall()
print("PANJANG NAMAAAAAAAAAAAAAAAAAAAAA WOIII : ", len(distinct_names))

# Prepare random data for other fields
cities = ['Padang', 'Palembang', 'Medan', 'Bandar Lampung', 'Tanjung Pinang', 'Banda Aceh', 'Pangkal Pinang', 'Bandung', 'Yogyakarta', 'Makassar', 'Surabaya', 'Tebing Tinggi', 'Sabang', ' Langsa', 'Ambon', 'Pontianak', 'Jayapura', 'Metro', 'Padang Sidempuan', 'Sawahlunto', 'Tanjung Balai', 'Prabumulih', 'LubukLinggau', 'Sibolga', 'Sibolga', 'Palu', 'Palangkaraya', 'Kendari', ' Mojokerto', 'Pasuruan', 'Pematang Siantar', 'Denpasar', 'Malang',' Pekanbaru','Semarang','Serang']
jobs = ['Editor', 'Paralegal', 'Budget/Accounting Analyst I', 'Senior Editor', 'Research Assistant II', 'Quality Engineer', 'Account Representative III', 'Engineer IV', 'Desktop Support Technician', 'Registered Nurse', 'Web Designer II', 'Senior Cost Accountant', 'Analog Circuit Design manager', 'Clinical Specialist', 'Legal Assistant', 'Professor', 'Quality Control Specialist', 'Help Desk Technician', 'Chief Design Engineer', 'Financial Advisor', 'Administrative Officer', 'Research Assistant II', 'Recruiting Manager', 'Human Resources Manager', 'Account Representative I', 'Physical Therapy Assistant', 'Quality Control Specialist', 'Health Coach III', 'Analyst Programmer', 'GIS Technical Architect', 'Office Assistant IV', 'Database Administrator II', 'Computer Systems Analyst I', 'Payment Adjustment Coordinator', 'Safety Technician IV', 'Environmental Specialist', 'Environmental Specialist', 'Structural Engineer', 'Sales Associate', 'Cost Accountant']
statuses = ['Belum Menikah', 'Menikah', 'Cerai']
blood_types = ['A', 'B', 'AB', 'O']
countries = ["Indonesia", "China", "China", "Namibia", "Bolivia", "Greece", "Japan", "South Korea", "China", "Russia", "Japan", "China", "Belarus", "United Kingdom", "China", "Comoros", "Portugal", "Egypt", "Ukraine", "Poland", "Russia", "Brazil", "China", "China", "Nigeria", "Indonesia", "China", "Azerbaijan", "United States", "Portugal", "Ukraine", "Indonesia", "Russia", "Indonesia", "Russia", "Sweden", "Cambodia", "Venezuela", "Iran", "Bolivia"]
genders = ['male', 'female']
addresses = ["719 Beilfuss Plaza", "73762 Kenwood Pass", "67 Warrior Court", "403 East Terrace", "70938 Gateway Street", "25987 Katie Way", "740 Manufacturers Point", "8955 Steensland Place", "204 Comanche Park", "197 Grayhawk Way", "2 Summit Court", "62 John Wall Terrace", "5565 Green Street", "88777 Warbler Avenue", "3553 Maple Pass", "01 Main Hill", "04 Larry Court", "2287 Reinke Lane", "19 Merchant Way", "45 Talisman Center", "797 Dwight Trail", "996 Rowland Drive", "8714 Dahle Parkway", "8701 Lakewood Gardens Crossing", "28409 Spaight Pass", "5778 Amoth Plaza", "0 Walton Parkway", "8 Northland Center", "9 Barby Place", "89 Gerald Street", "08 Hanson Alley", "9253 Melvin Terrace", "4 Mifflin Place", "4779 Homewood Pass", "9221 High Crossing Drive", "2483 Charing Cross Place", "544 Karstens Road", "7997 Upham Plaza", "5951 Cottonwood Plaza", "39490 Service Park"]
religions = ['Christian', 'Chatolic', 'Islam', 'Konghucu', 'Hindu', 'Buddha']

# Insert transformed names and random data into biodata table
for (name,) in distinct_names:
    alay_name = convert_alay(name)
    pk = ''.join(random.choices('0123456789', k=16))
    tempat_lahir = random.choice(cities)
    jenis_kelamin = random.choice(genders)
    golongan_darah = random.choice(blood_types)
    alamat = random.choice(addresses)
    status_perkawinan = random.choice(statuses)
    pekerjaan = random.choice(jobs)
    kewarganegaraan = random.choice(countries)
    agama = random.choice(religions)
    
    try:
        cursor.execute('''
            INSERT INTO biodata (PK, nama, tempat_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan)
            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
        ''', (pk, alay_name, tempat_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan))
        
        # print("Input success: ", alay_name)
    except sqlite3.Error as e:
        print(f"Error inserting {alay_name}: {e}")

# Commit changes and close the connection
conn.commit()
conn.close()

print("DONE")