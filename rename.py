import os

main_path   = "D:/vids/raw"
output_path = "D:/vids/forcite"

directories = os.listdir( main_path )

for sub_folder in os.listdir(main_path):
    if os.path.isdir(os.path.join(main_path, sub_folder)):
        #print(sub_folder)
        year   = sub_folder[:4]+'-'
        month  = sub_folder[4:6]+'-'
        day    = sub_folder[6:8]+'_'
        hour   = sub_folder[8:10]+'.'
        minute = sub_folder[10:12]+'.'
        #print ( year+month+day+hour+minute )
        sub_path = main_path + "/"+sub_folder
        videos = os.listdir( sub_path )
        videos.sort()
        for file in videos:
            end_bit = file[-10:] 
            #print(file, end_bit)
            source      = sub_path + "/" + file
            destination = output_path + "/" + year+month+day+hour+minute+end_bit
            #print (source, destination)
            os.rename(source, destination)
        os.rmdir(main_path+'/'+sub_folder)

        