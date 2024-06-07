import os
import random
from faker import Faker
import sqlite3

# Connect to SQLite database
db_path = 'test.db'
conn = sqlite3.connect(db_path)
cursor = conn.cursor()

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

# Create sidik_jari table
create_table_query = """
    CREATE TABLE IF NOT EXISTS sidik_jari (
      id INTEGER PRIMARY KEY AUTOINCREMENT,
      nama TEXT DEFAULT NULL,
      berkas_citra TEXT DEFAULT NULL
    );
"""

try:
    cursor.execute(create_table_query)
    print("sidik_jari table created successfully.")
except sqlite3.Error as err:
    print(f"Error creating table: {err}")

# Instantiate Faker
fake = Faker('id_ID')  # 'id_ID' locale for Indonesian data

# Directory containing BMP images
image_directory = './fingerprint'

# Function to get base name (name before underscore)
def get_base_name(filename):
    return filename.split('_', 1)[0]

# Function to read images from directory
def read_images_from_directory(directory):
    try:
        image_files = [f for f in os.listdir(directory) if f.endswith('.BMP')]
        return image_files
    except OSError as e:
        print(f"Error reading directory {directory}: {e}")
        return []

# Generate random Indonesian person's name with 1 to 3 words
def generate_random_name():
    return fake.name().replace('.', '').replace(',', '')  # Use fake.name() for Indonesian names

# Generate fake data and insert into sidik_jari table
def generate_fake_sidik_jari():
    image_files = read_images_from_directory(image_directory)

    if not image_files:
        print(f"No BMP images found in {image_directory}.")
        return

    print(f"Found {len(image_files)} BMP images in {image_directory}:")
    print(image_files)

    # Dictionary to store name based on number prefix
    number_prefix_names = {}

    for filename in image_files:
        base_name = get_base_name(filename)
        number_prefix = base_name.split('__', 1)[0]  # Get the number prefix
        person_name = number_prefix_names.get(number_prefix)

        if not person_name:
            person_name = generate_random_name()
            number_prefix_names[number_prefix] = person_name

        image_path = os.path.join(image_directory, filename)

        data = {
            'nama': person_name,
            'berkas_citra': filename  # Store the filename in berkas_citra
        }

        insert_query = """
            INSERT INTO sidik_jari (nama, berkas_citra)
            VALUES (:nama, :berkas_citra)
        """

        try:
            cursor.execute(insert_query, data)
            conn.commit()
            print(f"Inserted fake sidik_jari data for {filename} successfully.")
        except sqlite3.Error as err:
            print(f"Error inserting data for {filename}: {err}")

# Insert fake sidik_jari data into sidik_jari table
generate_fake_sidik_jari()

# Close connection
conn.close()