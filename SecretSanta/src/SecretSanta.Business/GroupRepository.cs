using System;
using System.Collections.Generic;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public class GroupRepository : IGroupRepository
    {
        private static Random rng = new Random();  
        public Group Create(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            MockData.Groups[item.Id] = item;
            return item;
        }

        public AssignmentResult GenerateGiftAssignment(int id)
        {
            Group group = MockData.Groups[id];
            List<User> usersInGroup = group.Users;

            if (usersInGroup.Count < 3) 
            {
                return AssignmentResult.Error(group.Name + "is not a valid group. A group must contain at least three users.");
            }

            Shuffle(usersInGroup);

            for (int i = 0; i < usersInGroup.Count; i ++) 
            {
                if (i < usersInGroup.Count - 1) 
                {
                    MockData.Groups[id].Assignments.Add(new Assignment(usersInGroup[i], usersInGroup[i + 1]));
                } 
                else 
                {
                    MockData.Groups[id].Assignments.Add(new Assignment(usersInGroup[i], usersInGroup[0]));
                }
            }

            return AssignmentResult.Success();
        }

        public Group? GetItem(int id)
        {
            if (MockData.Groups.TryGetValue(id, out Group? user))
            {
                return user;
            }
            return null;
        }

        public ICollection<Group> List()
        {
            return MockData.Groups.Values;
        }

        public bool Remove(int id)
        {
            return MockData.Groups.Remove(id);
        }

        public void Save(Group item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            MockData.Groups[item.Id] = item;
        }

       //Link to the location of a shuffle method that I found: https://stackoverflow.com/questions/273313/randomize-a-listt
       private void Shuffle<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            while (n > 1) 
            {  
                n--;  
                int k = rng.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
    }
}
