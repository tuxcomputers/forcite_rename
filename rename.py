import os
import shutil

helmet_drive = "E:"
video_loc   = "D:/vids/forcite"

helmet_path = helmet_drive+"/Forcite"

directories = os.listdir( helmet_path )

for sub_folder in os.listdir(helmet_path):
    if os.path.isdir(os.path.join(helmet_path, sub_folder)):
        #print(sub_folder)
        year   = sub_folder[:4]+'-'
        month  = sub_folder[4:6]+'-'
        day    = sub_folder[6:8]+'_'
        hour   = sub_folder[8:10]+'.'
        minute = sub_folder[10:12]+'.'
        #print ( year+month+day+hour+minute )
        sub_path = helmet_path + "/"+sub_folder
        videos = os.listdir( sub_path )
        videos.sort()
        for file in videos:
            end_bit = file[-10:] 
            #print(file, end_bit)
            source      = sub_path + "/" + file
            destination = video_loc + "/" + year+month+day+hour+minute+end_bit
            #print (source, destination)
            shutil.move(source, destination)
        os.rmdir(helmet_path+'/'+sub_folder)

print ('All done')
        