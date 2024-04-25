import matplotlib.pyplot as plt

# Define your cities and path
cities = {
    0: (0, 0),
    1: (-3138, -2512),
    2: (6804, -1072),
    3: (-193, 8782),
    4: (-5168, 2636),
    5: (-8022, -3864),
    6: (-9955, -2923),
    7: (-7005, 2118),
    8: (7775, -8002),
    9: (4244, -1339),
    10: (9478, -1973),
    11: (-7795, -5000),
    12: (-4521, 1266),
    13: (-192, 3337),
    14: (-9860, 1311),
    15: (-541, -5135),
    16: (-2385, 5987),
    #17: (-2492, 1009)
}

# Example path (you should update this with the actual order from your solver)
#path = [0, 1, 11, 5, 6, 14, 7, 12, 4, 3, 13, 9, 2, 10, 8, 0] #list
#path = [0,13,3,4,12,7,14,6,5,11,1,8,10,2,9,0] #optimal
#path = [0,13,12,4,7,14,6,5,11,1,9,2,10,8,3,0] #Nearest Neighbor
#path = [0,1,5,11,6,7,14,12,4,2,8,10,3,9,13,0] #AA
#path = [0,3,13,12,4,7,14,6,5,11,1,9,2,10,8,0] #MBF
#path = [0,1,11,5,6,14,7,12,4,3,13,9,2,10,8,0] #PMBF
#path= [0,3,13,12,4,7,14,6,5,11,1,15,9,2,10,8,0]

#start path for deliverables
#path=[0,9,2,10,8,1,5,6,7,4,3,0]
#path=[0,9,2,10,8,1,11,5,6,7,4,3,0]
#path=[0,13,12,4,7,14,6,5,11,1,9,2,10,8,3,0]
#path=[0,13,12,4,7,14,6,5,11,1,9,2,10,8,3,0] #nope
#path=[0,13,16,3,4,12,7,14,6,5,11,1,15,9,2,10,8,0]  #nope
#path=[0,17,12,4,7,14,6,5,11,19,15,1,13,16,3,9,18,2,10,8,0] #nope
#path=[0,13,12,4,7,14,6,5,11,1,9,2,10,8,3,0] #nope
#path=[0,1,5,11,6,7,14,12,4,2,8,10,3,9,13,0] #AA
#path=[0,3,13,12,4,7,14,6,5,11,1,15,9,2,10,8,0] #MBF 16
#path=[0,3,16,13,12,4,7,14,6,5,11,1,15,9,2,10,8,0] #MBF 17
#path=[0,13,12,4,7,14,6,5,11,1,15,9,2,10,8,3,0] #NN 16
#path=[0,13,16,3,4,12,7,14,6,5,11,1,15,9,2,10,8,0] #NN 17
#path=[0,1,5,11,6,7,14,12,4,15,8,2,10,3,9,13,0] #AA16
#path=[0,1,5,11,6,12,4,7,14,15,8,2,10,3,9,13,16,0] #AA17
#path=[0,9,2,10,8,15,1,11,5,6,14,7,4,12,13,3,0] #PMBF16
path=[0,13,3,16,4,12,7,14,6,5,11,1,15,9,2,10,8,0] #PMBF17

# Plot the cities
for city, (x, y) in cities.items():
    plt.plot(x, y, 'bo')  # 'bo' for blue dot
    plt.text(x, y, f' {city}', color='blue', fontsize=12)

# Draw lines for the path
for i in range(len(path)-1):
    x1, y1 = cities[path[i]]
    x2, y2 = cities[path[i+1]]
    plt.plot([x1, x2], [y1, y2], 'r')  # 'r' for red line

# Set plot features
plt.title('Traveling Salesman Path')
plt.xlabel('X Coordinate')
plt.ylabel('Y Coordinate')
plt.grid(True)
plt.axis('equal')  # Equal scaling of the plot axes.

# Show plot
plt.show()
