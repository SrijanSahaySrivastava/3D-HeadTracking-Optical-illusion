import cv2
import numpy as np
import mediapipe as mp
from cvzone.FaceDetectionModule import FaceDetector
import socket

cap = cv2.VideoCapture(0)
cap.set(3, 640)
cap.set(4, 480)

detector = FaceDetector(minDetectionCon=0.8)

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ('127.0.0.1', 5052)

while True:
    success, img = cap.read()
    img, bboxs = detector.findFaces(img)
    
    if bboxs:
        center = bboxs[0]['center']
        sock.sendto(str(center).encode(), serverAddressPort)
        print(center)
    
    cv2.imshow("Image", img)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break
    
cap.release()
cv2.destroyAllWindows()