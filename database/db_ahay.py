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
            
        if char != ' 'and (char == 'a' or char == 'i' or char == 'u' or char == 'e' or char == 'o') and random.random() < 0.1:  # 10% chance remove character
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
        tanggal_lahir VARCHAR(50),
        jenis_kelamin VARCHAR(6) CHECK (jenis_kelamin IN ('Male', 'Female')),
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
genders = ['Male', 'Female']
addresses = ["719 Beilfuss Plaza", "73762 Kenwood Pass", "67 Warrior Court", "403 East Terrace", "70938 Gateway Street", "25987 Katie Way", "740 Manufacturers Point", "8955 Steensland Place", "204 Comanche Park", "197 Grayhawk Way", "2 Summit Court", "62 John Wall Terrace", "5565 Green Street", "88777 Warbler Avenue", "3553 Maple Pass", "01 Main Hill", "04 Larry Court", "2287 Reinke Lane", "19 Merchant Way", "45 Talisman Center", "797 Dwight Trail", "996 Rowland Drive", "8714 Dahle Parkway", "8701 Lakewood Gardens Crossing", "28409 Spaight Pass", "5778 Amoth Plaza", "0 Walton Parkway", "8 Northland Center", "9 Barby Place", "89 Gerald Street", "08 Hanson Alley", "9253 Melvin Terrace", "4 Mifflin Place", "4779 Homewood Pass", "9221 High Crossing Drive", "2483 Charing Cross Place", "544 Karstens Road", "7997 Upham Plaza", "5951 Cottonwood Plaza", "39490 Service Park"]
religions = ['Christian', 'Chatolic', 'Islam', 'Konghucu', 'Hindu', 'Buddha']
ttls = ["8/18/2014","11/5/2001","8/7/2023","5/8/2021","10/4/2015","10/31/2015","12/18/2015","2/2/2024","7/19/2010","7/18/2006","9/19/2002","10/8/2019","6/15/2017","6/17/2021","6/26/2018","2/1/2010","12/31/2021","1/17/2017","4/5/2018","8/18/2019","3/22/2019","7/11/2014","5/28/2006","3/6/2010","12/31/2014","7/27/2004","11/12/2016","12/18/2009","2/2/2005","1/18/2014","10/18/2008","4/7/2024","9/20/2002","9/22/2004","9/29/2005","10/14/2014","5/30/2019","3/21/2011","1/10/2002","8/13/2011","8/8/2000","2/7/2024","5/2/2022","12/10/2011","4/30/2000","3/31/2023","8/1/2009","9/17/2021","4/7/2016","12/4/2014","11/8/2010","8/21/2014","7/5/2022","1/31/2019","9/16/2022","12/1/2011","2/8/2016","10/29/2004","11/10/2018","6/29/2004","3/29/2018","9/14/2011","7/26/2011","6/19/2015","1/25/2020","7/4/2009","8/20/2006","7/29/2015","10/29/2006","3/21/2007","4/9/2003","10/20/2007","7/10/2003","8/19/2021","7/4/2020","9/19/2022","5/27/2018","4/25/2002","12/24/2021","10/11/2002","7/16/2003","5/30/2024","5/3/2022","9/26/2021","1/13/2022","7/31/2022","3/9/2016","11/18/2012","10/13/2019","11/25/2015","6/29/2023","1/1/2000","2/3/2001","3/5/2012","9/2/2019","4/22/2020","3/14/2016","12/4/2011","10/15/2007","11/26/2018","7/23/2014","11/4/2004","5/2/2000","12/8/2008","3/12/2011","8/3/2008","3/7/2020","6/2/2019","7/29/2020","11/13/2003","9/15/2002","1/22/2001","12/16/2013","8/5/2013","6/30/2018","7/23/2009","7/3/2013","6/10/2009","3/15/2001","5/6/2018","2/21/2002","11/5/2004","4/27/2010","5/29/2021","5/1/2009","12/25/2012","1/3/2001","6/23/2020","8/23/2019","5/1/2008","11/23/2003","9/8/2022","8/3/2012","5/19/2016","6/16/2021","8/30/2020","7/29/2005","3/10/2003","10/21/2001","1/8/2024","9/22/2006","2/22/2020","12/21/2009","2/5/2007","6/27/2011","1/22/2019","8/12/2000","7/11/2001","11/10/2018","9/8/2000","1/23/2000","4/5/2009","4/10/2021","1/23/2008","8/1/2005","2/9/2009","11/8/2002","2/29/2000","8/11/2005","1/21/2018","3/25/2013","3/27/2004","8/25/2008","1/31/2019","9/23/2005","10/18/2014","8/11/2012","10/26/2009","5/19/2013","10/6/2021","2/4/2002","11/28/2014","5/19/2006","11/12/2005","4/5/2016","9/16/2019","11/1/2017","8/5/2022","8/28/2011","10/4/2014","6/6/2008","2/23/2017","7/16/2002","12/19/2008","10/25/2016","5/8/2014","3/19/2017","8/2/2016","8/24/2007","2/24/2011","11/5/2018","1/12/2016","1/14/2023","12/30/2017","11/26/2013","7/19/2009","12/1/2004","12/15/2019","5/2/2023","5/26/2022","7/31/2010","5/8/2008","6/1/2009","6/4/2013","7/14/2004","12/14/2005","10/14/2012","5/17/2002","11/27/2003","2/21/2011","7/15/2016","3/3/2016","10/14/2016","2/1/2019","1/11/2012","10/17/2006","5/28/2015","3/2/2014","12/24/2020","2/1/2021","8/1/2002","11/7/2010","10/13/2022","6/13/2014","5/26/2014","2/3/2017","6/28/2018","9/9/2014","9/4/2021","8/5/2013","3/12/2024","5/28/2019","4/2/2022","10/13/2017","1/23/2016","2/10/2000","4/20/2001","10/24/2008","4/10/2010","11/22/2010","10/20/2016","1/22/2006","10/1/2000","11/8/2012","9/17/2003","9/18/2023","3/13/2022","1/17/2004","1/11/2010","11/23/2015","10/4/2020","4/13/2006","5/31/2020","3/22/2011","8/25/2012","9/30/2015","6/9/2018","6/18/2014","10/4/2014","2/15/2024","6/23/2016","5/1/2009","7/12/2003","6/2/2019","6/4/2007","8/10/2004","5/21/2015","2/18/2005","9/29/2006","4/14/2014","7/6/2022","3/30/2010","1/9/2001","1/27/2018","12/1/2001","11/23/2011","2/27/2008","7/19/2013","6/5/2013","12/21/2003","5/25/2023","1/24/2020","12/27/2007","1/20/2012","9/14/2006","3/9/2005","7/14/2019","3/17/2012","9/28/2000","8/9/2004","1/21/2004","12/21/2021","10/27/2009","10/21/2008","5/11/2007","4/22/2012","9/13/2019","3/2/2015","5/24/2009","7/18/2022","11/23/2022","4/6/2009","8/4/2010","1/24/2005","10/13/2004","1/3/2017","9/28/2007","4/27/2005","11/16/2014","1/13/2005","10/7/2020","6/7/2013","3/20/2002","7/31/2011","7/29/2000","11/17/2013","6/25/2003","2/16/2013","11/1/2021","5/23/2017","12/16/2000","4/12/2008","3/7/2016","4/6/2018","1/28/2016","3/26/2000","12/26/2020","3/5/2004","2/11/2010","6/29/2007","9/2/2001","12/28/2006","3/30/2018","1/20/2002","6/14/2020","12/22/2015","8/11/2000","4/16/2019","2/6/2015","6/12/2014","5/17/2007","3/28/2009"]

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
    ttl = random.choice(ttls)
    
    
    try:
        cursor.execute('''
            INSERT INTO biodata (PK, nama, tempat_lahir, tanggal_lahir, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan)
            VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
        ''', (pk, alay_name, tempat_lahir, ttl, jenis_kelamin, golongan_darah, alamat, agama, status_perkawinan, pekerjaan, kewarganegaraan))
        
        # print("Input success: ", alay_name)
    except sqlite3.Error as e:
        print(f"Error inserting {alay_name}: {e}")

# Commit changes and close the connection
conn.commit()
conn.close()

print("DONE")