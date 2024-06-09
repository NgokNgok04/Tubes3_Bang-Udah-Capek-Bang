import os
def rename_files(folder_path):
    
    files = os.listdir(folder_path)
    
    
    for i, filename in enumerate(files):
        new_name = f"fingerprint{i+1001}.BMP"
        
        # Get the full path of the current and new file
        old_file = os.path.join(folder_path, filename)
        new_file = os.path.join(folder_path, new_name)

        # Rename the file
        os.rename(old_file, new_file)
    
    print(f"Renamed {len(files)} files successfully.")
    

folder_path = "D:\\Kuliah\\Semester 4\\Stima\\Tubes 3 (Fingerprint Detection)\\Tubes\\Tubes3_Bang-Udah-Capek-Bang\\test"

rename_files(folder_path)